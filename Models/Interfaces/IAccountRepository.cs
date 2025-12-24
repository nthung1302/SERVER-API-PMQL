using Files.Models.Entity;

namespace Files.Models.Interfaces
{
    public interface IAccountRepository
    {
        Account Login(string username, string password);
    }
}
