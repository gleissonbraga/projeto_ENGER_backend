using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class BudgetStage
    {
        [Key]
        public int StageId { get; private set; }
        public int BudgetId { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }
        public Status Status { get; private set; }

        public virtual Budget Budget { get; private set; }
        public virtual ICollection<BudgetMaterial> Materials { get; private set; } = new List<BudgetMaterial>();
        public virtual ICollection<BudgetLabor> Labors { get; private set; } = new List<BudgetLabor>();

        protected BudgetStage() { }

        public BudgetStage(string description, int order, Status status)
        {
            Description = description;
            Order = order;
            Status = status;
        }
    }
}