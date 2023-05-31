using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParserPlanBastard.Data;
using ParserPlanBastard.Models.DTO;
using ParserPlanBastard.Models.Entities;
using ParserPlanBastard.Models;
using System.Xml;
using ParserPlanBastard.Service;
using Microsoft.EntityFrameworkCore;

namespace ParserPlanBastard.Pages
{
    public class ParserPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IXmlRepository _xmlRepository;

        public ParserPageModel(ApplicationDbContext context, UserManager<User> userManager, IXmlRepository xmlRepository)
        {
            _context = context;
            _userManager = userManager;
            _xmlRepository = xmlRepository;
        }

        [BindProperty]
        public IEnumerable<Node> Node { get; set; }
        [FromRoute]
        [BindProperty(SupportsGet = true)]
        public string ButtonText { get; set; }

        public IXmlRepository XmlRepository => _xmlRepository;

        public IActionResult OnGet()
        {
            return Page();
        }
        public PartialViewResult OnGetRepeateOperations(string buttonText)
        {
            var file = _context.Files.FirstOrDefault(file => file.FileName == buttonText);

            if(file != null) 
            {
                _xmlRepository.FilePath= FindFilePath(buttonText);
                _xmlRepository.XmlDocument = new XmlDocument();
                return Partial("_CarPartial", _xmlRepository.Nodes);

            }
            return Partial("_CarPartial");
        }
        public string FindFilePath(string fileName)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            string[] files = Directory.GetFiles(basePath, fileName, SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                return files[0];
            }
            return null;
        }

        public PartialViewResult OnGetBtnClick(string buttonText)
        {
            var dtos = new List<DTOXmlRepresentation>();
            foreach (var item in _xmlRepository.Nodes)
            {
                if (buttonText == item.ShortName)
                {
                    dtos.Add(new DTOXmlRepresentation(item.ShortName, item.FullName, item.Attributes));
                    return Partial("_AttributeRepresentation", dtos);
                }

            }

            var node = FindNode(_xmlRepository.Nodes, buttonText);

            if (node != null)
            {
                dtos.Add(new DTOXmlRepresentation(node.ShortName, node.FullName, node.Attributes));
            }

            if (dtos.Any())
            {
                return Partial("_AttributeRepresentation", dtos);
            }
            return null;
        }


       

        private Node FindNode(IEnumerable<Node> nodes, string buttonText)
        {
            foreach (var node in nodes)
            {
                if (buttonText == node.ShortName)
                {
                    return node;
                }
                var childNode = FindNode(node.ChildNodes, buttonText);
                if (childNode != null)
                {
                    return childNode;
                }
            }
            return null;
        }
        

        public async Task<PartialViewResult> OnPost(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (file != null)
            {
                try
                {
                    if (await _xmlRepository.UploadFile(file))
                    {
                        ViewData["Message"] = "File Upload Successful";
                    }
                    else
                    {
                        ViewData["Message"] = "File Upload Failed";
                    }
                }
                catch (Exception)
                {

                    ViewData["Message"] = "File Upload Failed";
                }
                string projectPath = Directory.GetCurrentDirectory();
                string uploadPath = Path.Combine(projectPath, "UploadedFiles", file.FileName);
                _xmlRepository.FilePath = uploadPath;

                _xmlRepository.XmlDocument = new XmlDocument();

                string basePath = Path.Combine(Environment.CurrentDirectory, "UploadedFiles");
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string relativePath = Path.Combine("UploadedFiles", fileName);
                string fullPath = Path.Combine(basePath, fileName);



                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var newFile = new Models.Entities.File
                {
                    Hash = Models.Entities.File.ByteArrayToString(Models.Entities.File.ComputeAHash(fullPath)),
                    FileName= fileName,
                    FilePath = fullPath,
                    FileExtension =Path.GetExtension(file.FileName),
                    VolumeFile = file.Length,
                    UserId = user.Id
                };

                _context.Files.Add(newFile);

                try
                {

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }

                var newLog = new Logging
                {
                    Date = DateTime.Now,
                    UserId = user.Id,
                    FileId = newFile.Id
                };

                try
                {
                    _context.Logging.Add(newLog);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }



                return Partial("_CarPartial", _xmlRepository.Nodes);
            }

            else
            {
                return Partial("Empty");
            }



        }
        
        public async Task<List<Logging>> GetLoggingsByUserId()
        {
            var userName = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(userName);
            var userId = user.Id;
            return await _context.Logging
                .Where(logging => logging.UserId == userId)
                .Include(logging => logging.File)
                .ToListAsync();
        }
    }
}
