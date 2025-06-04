using System.ComponentModel.DataAnnotations;

namespace myCleanArchitecture.Domain.Common
{
    public abstract class BaseEntity<T> : IBaseEntity
    {
        [Key]// this attribute maybe removed if using EF Core conventions in infrastructure configuration
        public T Id { get; set; } = default!;
    }
}
