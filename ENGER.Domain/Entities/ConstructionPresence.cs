using System;
using System.ComponentModel.DataAnnotations;

namespace ENGER.Domain.Entities
{
    public class ConstructionPresence
    {
        [Key]
        public int PresenceId { get; private set; }
        public int? EmployeeId { get; private set; }
        public int? ConstructionId { get; private set; }
        public int? UserId { get; private set; }
        public DateTime? PresenceDate { get; private set; }
        public bool IsPresent { get; private set; } // Mapeado do BIT (IN_PRESENTE)

        // Navigation Properties
        public virtual Employee? Employee { get; private set; }
        public virtual Construction? Construction { get; private set; }
        public virtual User? User { get; private set; }

        private ConstructionPresence() { }

        // 🧠 Constructor
        public ConstructionPresence(
            int? employeeId,
            int? constructionId,
            int? userId,
            DateTime? presenceDate,
            bool isPresent
        )
        {
            EmployeeId = employeeId;
            ConstructionId = constructionId;
            UserId = userId;
            PresenceDate = presenceDate;
            IsPresent = isPresent;
        }
    }
}