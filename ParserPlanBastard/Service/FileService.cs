using ParserPlanBastard.Data;
using System.Text;
using XSystem.Security.Cryptography;
using ParserPlanBastard.Models.Entities;
using File = ParserPlanBastard.Models.Entities.File;

namespace ParserPlanBastard.Service
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;

        public FileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFileAsync(File newFile)
        {
            _context.Files.Add(newFile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(int fileId)
        {
            var file = await _context.Files.FindAsync(fileId);
            file.IsDeleted = true;

            await _context.SaveChangesAsync();
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
