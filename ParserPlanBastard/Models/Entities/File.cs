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

        public static byte[] ComputeAHash(string file)
        {
            byte[] tmpSource;
            byte[] tmpNewHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(file);

            tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            return tmpNewHash;
        }

        public static bool CheckHash(byte[] firstHash, byte[] secondHash)
        {
            bool bEqual = false;
            if (secondHash.Length == firstHash.Length)
            {
                int i = 0;
                while ((i < secondHash.Length) && (secondHash[i] == firstHash[i]))
                {
                    i += 1;
                }
                if (i == secondHash.Length)
                {
                    return bEqual = true;
                }
                else
                    return bEqual = false;
            }
            return bEqual = false;
        }




        public static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
