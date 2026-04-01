using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class ConstructionStage
    {
        [Key]
        public int StageId { get; private set; }
        public string? Description { get; private set; }
        public int? Order { get; private set; } // NR_ORDEM
        public int? ConstructionId { get; private set; }
        public int? Status { get; private set; } // SMALLINT no JSON

        // Navigation Property
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