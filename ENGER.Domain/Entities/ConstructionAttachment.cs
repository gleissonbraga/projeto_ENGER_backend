using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class ConstructionAttachment
    {
        [Key]
        public int ConstructionAttachmentId { get; private set; }
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public int? ConstructionId { get; private set; }

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