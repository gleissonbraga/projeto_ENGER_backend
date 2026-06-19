using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Repositories
{
    public class SendEmailRepository : IEmailService
    {
        private readonly AppDbContext _context;
        private readonly IResend _resend;

        public SendEmailRepository(AppDbContext context, IResend resend)
        {
            _context = context;
            _resend = resend;
        }

        public async Task<List<SendEmail>> GetEmailsNoSend()
        {
            List<SendEmail> lstEmails = await _context.SendEmails.Where(x => x.Status == Domain.Enums.Status.EmailNotSent).OrderBy(x => x.RecordDate).Take(10).ToListAsync();

            return lstEmails;
        }

        public async Task RecordEmail(SendEmail email)
        {
            await _context.SendEmails.AddAsync(email);
            _context.SaveChanges();
        }

        public async Task SendEmailAsync(string to, string subject, string body, byte[] attachment = null, string fileName = null)
        {
            // 1. Verifique se o cliente da Resend foi injetado (opcional, mas seguro)
            if (_resend == null) throw new Exception("O serviço Resend não foi inicializado.");

            var message = new Resend.EmailMessage();

            message.From = "onboarding@resend.dev";
            message.To.Add(to);
            message.Subject = subject;
            message.HtmlBody = body;

            if (attachment != null)
            {
                // 2. CORREÇÃO: Inicialize a lista se ela estiver nula
                if (message.Attachments == null)
                {
                    message.Attachments = new List<Resend.EmailAttachment>();
                }

                message.Attachments.Add(new Resend.EmailAttachment
                {
                    Filename = fileName ?? "documento.pdf",
                    Content = Convert.ToBase64String(attachment)
                });
            }

            await _resend.EmailSendAsync(message);
        }

        public async Task UpdateStatusEmail(SendEmail email)
        {
            _context.SendEmails.Update(email);
            _context.SaveChanges();
        }
    }
}
