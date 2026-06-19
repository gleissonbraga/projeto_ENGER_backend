using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string DescriptionPosition { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        protected Position() { }

        public Position(string descriptionPosition, int companyId)
        {
            DescriptionPosition = descriptionPosition;
            CompanyId = companyId;
        }
    }
}
