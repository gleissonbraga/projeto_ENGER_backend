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

        public virtual Budget Budget { get; private set; }
        public virtual ICollection<BudgetMaterial> Materials { get; private set; } = new List<BudgetMaterial>();
        public virtual ICollection<BudgetLabor> Labors { get; private set; } = new List<BudgetLabor>();

        protected BudgetStage() { }

        public BudgetStage(int budgetId, string description, int order)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new DomainException("Etapa", "Descrição da etapa é obrigatória.");
            BudgetId = budgetId;
            Description = description;
            Order = order;
        }
    }
}