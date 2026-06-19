using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class BudgetMaterial
    {
        [Key]
        public int BudgetMaterialId { get; private set; }
        public int StageId { get; private set; }
        public string Description { get; private set; }
        public string Unit { get; private set; }
        public decimal PlannedQuantity { get; private set; }
        public decimal UnitCost { get; private set; }
        public bool IsClientProvided { get; private set; } // O "Plus" que discutimos

        protected BudgetMaterial() { }

        public BudgetMaterial(string description, string unit, decimal plannedQuantity, decimal unitCost, bool isClientProvided)
        {
            Description = description;
            Unit = unit;
            PlannedQuantity = plannedQuantity;
            UnitCost = isClientProvided ? 0 : unitCost;
            IsClientProvided = isClientProvided;
        }
    }
}