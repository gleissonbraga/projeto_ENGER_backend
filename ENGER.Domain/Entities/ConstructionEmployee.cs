using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENGER.Domain.Entities
{
    public class ConstructionEmployee
    {
        [Key]
        public int ConstructionEmployeeId { get; private set; }
        public int? EmployeeId { get; private set; }
        public int? ConstructionId { get; private set; }

        public virtual Employee? Employee { get; private set; }
        [JsonIgnore]
        public virtual Construction? Construction { get; private set; }

        private ConstructionEmployee() { }

        public ConstructionEmployee(int? employeeId, int? constructionId)
        {
            EmployeeId = employeeId;
            ConstructionId = constructionId;
        }
    }
}