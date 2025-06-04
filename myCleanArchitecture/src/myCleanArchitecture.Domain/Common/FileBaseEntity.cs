namespace myCleanArchitecture.Domain.Common
{
    public class FileBaseEntity<T> : DetailedBaseEntity<T>, IFileBaseEntity, IDetailedBaseEntity
    {
        public string FileUrl { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public FileTypeEnum FileTypeEnum { get; set; }
        public long FileSize { get; set; }
    }
}
