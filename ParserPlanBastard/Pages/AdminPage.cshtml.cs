using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParserPlanBastard.Data;
using ParserPlanBastard.Models.Entities;
using ParserPlanBastard.Service;
using System.Data.Entity;

namespace ParserPlanBastard.Pages
{
    [Authorize("StatusAdmin")]
    public class AdminPageModel : PageModel
    {
        private readonly IFileService _fileService;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminPageModel(IFileService fileService, UserManager<User> userManager, ApplicationDbContext context)
        {
            _fileService = fileService;
            _userManager = userManager;
            _context = context;
        }

        public List<Logging> GetLoggingsByUserIdAsync()
        {
            

                return  _context.Logging
                    .Include(log => log.File)
                    .Include(log => log.User)
                    .Where(log => !log.File.IsDeleted)
                    .ToList();
            
           
        }

       
        public async Task<IActionResult> OnPostDeleteFile(int fileId)
        {
            await _fileService.DeleteFileAsync(fileId);

            return RedirectToAction("AdminPage");
        }
    }
}
