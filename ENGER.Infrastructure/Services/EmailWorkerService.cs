using ENGER.Domain.Entities;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Infrastructure.Data.Context;
using ENGER.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ENGER.Infrastructure.Services
{
    public class EmailWorkerService : BackgroundService
    {
        private readonly SendEmailRepository _sendEmailRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailWorkerService> _logger;
        private readonly int _intervalMinutes = 2;

        public EmailWorkerService(IServiceProvider serviceProvider, ILogger<EmailWorkerService> logger, SendEmailRepository sendEmailRepository)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _sendEmailRepository = sendEmailRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de Fila de E-mail iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        IEnumerable<SendEmail> lstEmails = await _sendEmailRepository.GetEmailsNoSend();

                        foreach (var email in lstEmails)
                        {
                            _logger.LogInformation($"Processando e-mail ID: {email.EmailId}");

                            await emailService.SendEmailAsync(email.To, email.Subject, email.Body);

                            email.Status = Domain.Enums.Status.EmailSent;
                            email.SentAt = DateTime.UtcNow;

                            await _sendEmailRepository.UpdateStatusEmail(email);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao processar fila de e-mail: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes), stoppingToken);
            }
        }
    }
}