using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENGER.Domain.Entities
{
    public class ConstructionAttachment
    {
        [Key]
        public int ConstructionAttachmentId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? ConstructionId { get; set; }

        [JsonIgnore]
        public virtual Construction? Construction { get; private set; }

        private ConstructionAttachment() { }

        public ConstructionAttachment(string? description, string? imageUrl, int? constructionId)
        {
            Description = description;
            ImageUrl = imageUrl;
            ConstructionId = constructionId;
        }
    }
}