using ENGER.Domain.Entities;
using ENGER.Domain.Services;

namespace ENGER.Application.UseCases.Auth.GetLoggedUser;

public class GetLoggedUserUseCase
{
    private readonly IUserContext _userContext;

    public GetLoggedUserUseCase(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public LoggedUserResponse Execute()
    {
        return new LoggedUserResponse
        {
            UserName = _userContext.GetUserName(),
            AdminLevel = _userContext.GetAdminLevel(),
            CompanyId = _userContext.GetCompanyId()
        };
    }
}

public class LoggedUserResponse
{
    public string UserName { get; set; } = string.Empty;
    public int AdminLevel { get; set; }
    public int CompanyId { get; set; }
}