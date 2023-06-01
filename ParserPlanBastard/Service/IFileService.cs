using ParserPlanBastard.Models.Entities;
using File = ParserPlanBastard.Models.Entities.File;

namespace ParserPlanBastard.Service
{
    public interface IFileService
    {
        public Task AddFileAsync(File newFile);
        public Task DeleteFileAsync(int fileId);
        public string FindFilePath(string fileName);
        public byte[] ComputeAHash(string file);
        public bool CheckHash(byte[] firstHash, byte[] secondHash);
        public string ByteArrayToString(byte[] arrInput);
    }
}
