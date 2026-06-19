using ENGER.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Domain.Entities
{
    public class SendEmail
    {
        public Guid EmailId { get;  set; }
        public string To { get;  set; }
        public string Subject { get;  set; }
        public string Body { get;  set; }
        public byte[]? AttachmentContent { get; private set; }
        public string? FileName { get; set; }
        public DateTime SentAt { get;  set; }
        public DateTime RecordDate { get;  set; }
        public Status Status { get;  set; }

        private SendEmail() { }

        public SendEmail(string to, string subject, string body, Status status, DateTime recordDate, string fileName, byte[]? attachmentContent = null)
        {
            EmailId = Guid.NewGuid();
            To = to;
            Subject = subject;
            Body = body;
            Status = status;
            RecordDate = recordDate;
            FileName = fileName;
            AttachmentContent = attachmentContent; // Agora aceita os bytes do PDF
        }
    }
}
