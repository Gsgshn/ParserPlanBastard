using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParserPlanBastard.Models.Entities
{
    public class File
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Hash { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public long VolumeFile { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Element> Elements { get; set; }
        public ICollection<Logging> Loggings { get; set; }

        //        string sSourceData;
        //        byte[] tmpSource;
        //        byte[] tmpHash;
        //         sSourceData = "MySourceData";
        //            //Create a byte array from source data
        //            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

        //            //Compute hash based on source data
        //            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        //        Console.WriteLine(ByteArrayToString(tmpHash));

        //            sSourceData = "NotMySourceData";
        //            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

        //            byte[] tmpNewHash;

        //        tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

        //        bool bEqual = false;
        //            if (tmpNewHash.Length == tmpHash.Length)
        //            {
        //                int i = 0;
        //                while ((i<tmpNewHash.Length) && (tmpNewHash[i] == tmpHash[i]))
        //                {
        //                    i += 1;
        //                }
        //                if (i == tmpNewHash.Length)
        //                {
        //                    bEqual = true;
        //                }
        //            }

        //            if (bEqual)
        //    Console.WriteLine("The two hash values are the same");
        //else
        //    Console.WriteLine("The two hash values are not the same");
        //Console.ReadLine();
        //        }

        //        static string ByteArrayToString(byte[] arrInput)
        //{
        //    int i;
        //    StringBuilder sOutput = new StringBuilder(arrInput.Length);
        //    for (i = 0; i < arrInput.Length - 1; i++)
        //    {
        //        sOutput.Append(arrInput[i].ToString("X2"));
        //    }
        //    return sOutput.ToString();
        //}
    }
}
