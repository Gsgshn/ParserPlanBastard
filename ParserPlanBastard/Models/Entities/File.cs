using System.Text;
using XSystem.Security.Cryptography;

namespace ParserPlanBastard.Models.Entities
{
    public class File
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Hash { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long VolumeFile { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Element> Elements { get; set; }
        public ICollection<Logging> Loggings { get; set; }

        
    }
}
