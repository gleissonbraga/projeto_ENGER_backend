using ENGER.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Interfaces.Repositories
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, byte[] attachment = null, string fileName = null);
        Task RecordEmail(SendEmail email);
        Task UpdateStatusEmail(SendEmail email);
        Task<List<SendEmail>> GetEmailsNoSend();

    }
}
