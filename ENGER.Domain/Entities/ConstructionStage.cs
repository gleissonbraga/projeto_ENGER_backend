using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENGER.Domain.Entities
{
    public class ConstructionStage
    {
        [Key]
        public int StageId { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; } // NR_ORDEM
        public int? ConstructionId { get; set; }
        public int? Status { get; set; } // SMALLINT no JSON

        // Navigation Property
        [JsonIgnore]
        public virtual Construction? Construction { get; private set; }

        private ConstructionStage() { }

        // 🧠 Constructor
        public ConstructionStage(
            string? description,
            int? order,
            int? constructionId,
            int? status
        )
        {
            Description = description;
            Order = order;
            ConstructionId = constructionId;
            Status = status;
        }
    }
}