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
        // 1. Removido o campo do repositório daqui
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailWorkerService> _logger;
        private readonly int _intervalMinutes = 2;

        // 2. Removido o repositório dos parâmetros do construtor
        public EmailWorkerService(IServiceProvider serviceProvider, ILogger<EmailWorkerService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(">>> MONITOR: Serviço de Fila de E-mail iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($">>> MONITOR: Verificando fila de e-mails às {DateTime.Now:HH:mm:ss}");
                
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        // 3. Agora buscamos o repositório e o serviço de e-mail dentro do escopo
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        var repo = scope.ServiceProvider.GetRequiredService<SendEmailRepository>();

                        var lstEmails = await repo.GetEmailsNoSend();

                        if (lstEmails == null || !lstEmails.Any())
                        {
                            _logger.LogInformation(">>> MONITOR: Nenhum e-mail pendente encontrado.");
                        }
                        else
                        {
                            foreach (var email in lstEmails)
                            {
                                _logger.LogInformation($">>> MONITOR: Enviando e-mail {email.EmailId} para {email.To}");

                                // Envia usando os bytes que estão na entidade
                                await emailService.SendEmailAsync(email.To, email.Subject, email.Body, email.AttachmentContent, email.FileName);

                                email.Status = Domain.Enums.Status.EmailSent;
                                email.SentAt = DateTime.UtcNow;

                                await repo.UpdateStatusEmail(email);
                                _logger.LogInformation($">>> MONITOR: Status atualizado para e-mail {email.EmailId}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($">>> MONITOR ERRO: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes), stoppingToken);
            }
        }
    }
}