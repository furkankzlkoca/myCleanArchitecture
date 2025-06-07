using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace myCleanArchitecture.API.Helpers.Models
{
    public class MultipleFileUploadWithEntityModel<TEntity>
    {
        [FromForm(Name = "files")]
        public List<IFormFile>? Files { get; set; }

        [FromForm(Name = "entity")]
        public string EntityJson { get; set; }
        public TEntity Entity => JsonConvert.DeserializeObject<TEntity>(EntityJson) ?? throw new ArgumentNullException(nameof(EntityJson), "Entity cannot be null.");
        // TODO: check if the entity is null after deserialization and handle it accordingly
    }
}
