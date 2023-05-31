using System.Text;
using XSystem.Security.Cryptography;

namespace ParserPlanBastard.Service
{
    public class FileService : IFileService
    {
        public string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public bool CheckHash(byte[] firstHash, byte[] secondHash)
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

        public byte[] ComputeAHash(string file)
        {
            throw new NotImplementedException();
        }

        //public byte[] ComputeAHash(string file)
        //{
        //    byte[] tmpSource;
        //    byte[] tmpNewHash;

        //    tmpSource = Encoding.ASCII.GetBytes(file);

        //    tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

        //    return tmpNewHash;
        //}
    }
}
