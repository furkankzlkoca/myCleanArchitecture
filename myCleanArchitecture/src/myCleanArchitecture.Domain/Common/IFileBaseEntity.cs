namespace myCleanArchitecture.Domain.Common
{
    public interface IFileBaseEntity
    {
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public FileTypeEnum FileTypeEnum { get; set; }
        public long FileSize { get; set; }
    }
}
