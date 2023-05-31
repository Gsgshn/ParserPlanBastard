namespace ParserPlanBastard.Service
{
    public interface IFileService
    {
        public byte[] ComputeAHash(string file);
        public bool CheckHash(byte[] firstHash, byte[] secondHash);
        public string ByteArrayToString(byte[] arrInput);
    }
}
