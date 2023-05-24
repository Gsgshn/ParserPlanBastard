using Microsoft.AspNetCore.Http;
using ParserPlanBastard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParserPlanBastard.Service
{
    public interface IXmlRepository
    {
        public XmlDocument XmlDocument { get; set; }
        public List<ElementStringRepresentation> ElementStringRepresentations { get; set; }
        public List<Node> Nodes { get; set; }
        public Node SelectedNode { get; set; }
        public string FilePath { get; set; }
        public void LoadElementStringRepresentationFromJson();
        public Node GetRootNode(XmlDocument xmlDocument);
        public Node MapXmlNode(XmlNode xmlNode);
        public string GetFullName(string shortName);
        public Task<bool> UploadFile(IFormFile file);
        public string GetFileSizeInKilobytes(IFormFile file);
       

    }
}
