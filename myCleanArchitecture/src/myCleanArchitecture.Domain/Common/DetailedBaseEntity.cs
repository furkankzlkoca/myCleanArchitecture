namespace myCleanArchitecture.Domain.Common
{
    public abstract class DetailedBaseEntity<T> : BaseEntity<T>, IDetailedBaseEntity
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }=default!;
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}
