using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Admin Admin { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Status? Status { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        protected User() { }

        public User(string username, string email, string password, Admin admin, DateTime entryDate, DateTime updateDate, int companyId, Status status)
        {
            Username = username;
            Email = email;
            Password = password;
            Admin = admin;
            EntryDate = entryDate;
            UpdateDate = updateDate;
            CompanyId = companyId;
            Status = status;
        }
    }
}
