using ENGER.Application.DTOs.Company;
using ENGER.Application.DTOs.User;
using ENGER.Application.Exceptions;
using ENGER.Domain.Entities;
using ENGER.Domain.Enums;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using System.Text.Json;


namespace ENGER.Application.UseCases.Company.Create
{
    public class UpdatePaymentUseCase
    {
        private readonly ICompanyRepository _repository;
        private readonly IPaymentServiceRepository _paymentRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public UpdatePaymentUseCase(ICompanyRepository repository, IPaymentServiceRepository paymentRepository, ISubscriptionRepository subscriptionRepository)
        {
            _repository = repository;
            _paymentRepository = paymentRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task ExecuteAsync(JsonElement payload)
        {
            (int status, string subscriptionCode) = await _paymentRepository.ReceivedNotification(payload);

            if (string.IsNullOrEmpty(subscriptionCode) && status == 0) throw new ApplicException("payment", "Erro ao efetuar o pagamento.");

            Domain.Entities.Subscription objSubscription = await _subscriptionRepository.GetBySubscriptionKeyAccess(Guid.Parse(subscriptionCode));

            DateTime dataAtualUtc = DateTime.UtcNow;

            if (status == (int)Status.SubActive && objSubscription.PaymentDate?.Date != dataAtualUtc.Date)
            {
                objSubscription.StartDate = DateTime.Now;
                objSubscription.PaymentDate = DateTime.Now;
                objSubscription.ExpirationDate = DateTime.UtcNow.AddDays(30);
                objSubscription.NextBillingDate = DateTime.UtcNow.AddMonths(1);
            }

            objSubscription.StatusSubscription = (Status)status;

            await _subscriptionRepository.UpdateAsync(objSubscription);
        }
    }
}
