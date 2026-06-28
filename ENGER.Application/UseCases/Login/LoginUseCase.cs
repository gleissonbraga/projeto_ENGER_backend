using ENGER.Application.DTOs.Login;
using ENGER.Application.Exceptions;
using ENGER.Domain.Exceptions;
using ENGER.Domain.Interfaces.Repositories;
using ENGER.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENGER.Application.UseCases.Login
{
    public class LoginUseCase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public LoginUseCase(IUserRepository repository, ITokenService tokenService, IPasswordService passwordService, ISubscriptionRepository subscriptionRepository)
        {
            _repository = repository;
            _tokenService = tokenService;
            _passwordService = passwordService;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<LoginResponseDTO> ExecuteAsync(LoginRequestDTO request)
        {

            Validate(request);

            var user = await _repository.GetByEmailLogin(request.Email);

            if (user == null || !_passwordService.VerifyPassword(request.Password, user.Password))
                throw new ApplicException("login", "E-mail ou senha inválidos.");

            if(user.Company.SubscriptionCode == null)
                throw new ApplicException("login", "Sua assinatura está inválida.");

            Domain.Entities.Subscription subscription = await _subscriptionRepository.GetBySubscriptionKeyAccess((Guid)user.Company.SubscriptionCode);

            if (DateTime.UtcNow > subscription.ExpirationDate)
                throw new ApplicException("login", "Sua assinatura está vencida");

            var token = _tokenService.GenerateToken(user, subscription.TypeSubscriptionId);

            return new LoginResponseDTO(token, user.Username, user.Admin, user.CompanyId, subscription.ExpirationDate, subscription.TypeSubscriptionId);
        }

        private void Validate(LoginRequestDTO request)
        {
            var errors = new List<ValidationError>();

            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                errors.Add(new ValidationError("email", "O campo e-mail é obrigatório."));
            }
            else if (!Regex.IsMatch(request.Email, emailRegex))
            {
                errors.Add(new ValidationError("email", "O formato do e-mail é inválido."));
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                errors.Add(new ValidationError("password", "O campo senha é obrigatório."));
            }
            else if (request.Password.Length < 4)
            {
                errors.Add(new ValidationError("password", "A senha deve conter pelo menos 6 caracteres."));
            }

            if (errors.Any())
            {
                throw new ApplicException(errors);
            }
        }
    }
}
