using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ParserPlanBastard.Models;
using System.Reflection;
using System.Text;
using System.Xml;


namespace ParserPlanBastard.Service
{
    public class XmlFileRepresentation : IXmlRepository
    {
        private XmlDocument _xmlDocument;
        private List<ElementStringRepresentation> _elementStringRepresentations;
        private List<Node> _nodes;
        private Node _selectedNode;

        /// <summary>
        /// add check
        /// </summary>
        public XmlDocument XmlDocument

        {
            get => _xmlDocument;

            set //add check
            {
                _xmlDocument = value;
                _nodes.Clear();
                _xmlDocument.Load(FilePath);
                Nodes.Add(GetRootNode(_xmlDocument));
            }
        }





        public List<ElementStringRepresentation> ElementStringRepresentations
        { get => _elementStringRepresentations; set => _elementStringRepresentations = value; }
        public List<Node> Nodes { get => _nodes; set => _nodes = value; }
        public Node SelectedNode { get => _selectedNode; set => _selectedNode = value; }
        public string FilePath { get; set; }



        //по дефолту переводит регистр
        public XmlFileRepresentation()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LoadElementStringRepresentationFromJson();
            Nodes = new List<Node>();
        }


        //получение корневого узла
        public Node GetRootNode(XmlDocument xmlDocument)
        {
            var rootXmlNode = xmlDocument.DocumentElement;
            var rootNode = MapXmlNode(rootXmlNode);

            return rootNode;
        }

        //подгрузка json файла
        public void LoadElementStringRepresentationFromJson()
        {
            using (StreamReader r = new StreamReader("invoice.json"))
            {
                var json = r.ReadToEnd();
                _elementStringRepresentations = JsonConvert.DeserializeObject<List<ElementStringRepresentation>>(json);
            }
        }

        //парс
        public Node MapXmlNode(XmlNode xmlNode)
        {
            var shortName = xmlNode.Name == "#text" ? xmlNode.Value : xmlNode.Name;

            var node = new Node(shortName, GetFullName(shortName), xmlNode.Value);

            var attributeCount = xmlNode.Attributes?.Count ?? 0;

            for (int i = 0; i < attributeCount; i++)
            {
                var item = xmlNode.Attributes[i];
                var a = new Models.Attribute(node, item.Value, GetFullName(item.Name), item.Value);
                node.Attributes.Add(a);
            }
            var childCount = xmlNode.ChildNodes?.Count ?? 0;

            for (int i = 0; i < childCount; i++)
            {
                var item = xmlNode.ChildNodes[i];
                var n = MapXmlNode(item);
                node.ChildNodes.Add(n);
            }

            return node;
        }




        public async Task<bool> UploadFile(IFormFile file)
        {

            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        public string GetFullName(string shortName)
        {
            return ElementStringRepresentations
                .SingleOrDefault(s => s.ShortName == shortName)
                ?.FullName ?? shortName;
        }
        public string GetFileSizeInKilobytes(IFormFile file) // возможная ошибка
        {
            double fileSizeInBytes = file.Length;
           
                double fileSizeInKilobytes = fileSizeInBytes / 1024;
                return fileSizeInKilobytes.ToString();
           
            
        }
        public static string BytesToString(long byteCount)
        {
            string[] suf = { "Byt", "KB", "MB", "GB", "TB", "PB", "EB" }; //
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
