using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class BudgetLabor
    {
        [Key]
        public int BudgetLaborId { get; private set; }
        public int StageId { get; private set; }
        public int RoleId { get; private set; } // CD_CARGO
        public decimal PlannedHours { get; private set; }
        public decimal HourlyRate { get; private set; }
        public decimal SocialCharges { get; private set; }

        protected BudgetLabor() { }

        public BudgetLabor(int stageId, int roleId, decimal plannedHours, decimal hourlyRate, decimal socialCharges)
        {
            StageId = stageId;
            RoleId = roleId;
            PlannedHours = plannedHours;
            HourlyRate = hourlyRate;
            SocialCharges = socialCharges;
        }
    }
}