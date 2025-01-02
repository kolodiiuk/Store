namespace Laundry.Domain.Contracts.Services;

public interface IAuthService
{
    void Register(string email, string password);
    void Login(string email, string password);
}
