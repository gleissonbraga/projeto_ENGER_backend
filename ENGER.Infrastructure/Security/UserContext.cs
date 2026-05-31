using System.Security.Claims;
using ENGER.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace ENGER.Infrastructure.Security;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetCompanyId()
    {
        // Pega a claim "CompanyId" que você injetou lá no seu TokenService
        var claim = _httpContextAccessor.HttpContext?.User.FindFirst("CompanyId")?.Value;

        if (string.IsNullOrEmpty(claim) || !int.TryParse(claim, out var companyId))
        {
            throw new Exception("Contexto de empresa não encontrado na requisição.");
        }

        return companyId;
    }

    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
    }

    public int GetAdminLevel()
    {
        // Pega a claim "AdminLevel" que você injetou lá no seu TokenService
        var claim = _httpContextAccessor.HttpContext?.User.FindFirst("AdminLevel")?.Value;
        return int.TryParse(claim, out var level) ? level : 1;
    }

    public int GetUserId()
    {
        // Pega a claim "AdminLevel" que você injetou lá no seu TokenService
        var claim = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(claim) || !int.TryParse(claim, out var userId))
        {
            var todasAsClaims = string.Join(" | ", _httpContextAccessor.HttpContext?.User.Claims.Select(c => $"{c.Type} = {c.Value}") ?? Array.Empty<string>());
            throw new Exception("Contexto de usuário não encontrado na requisição.");
        }

        return userId;
    }
}