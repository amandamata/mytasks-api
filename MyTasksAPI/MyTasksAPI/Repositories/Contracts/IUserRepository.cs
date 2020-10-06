using MyTasks.Models;

namespace MyTasks.Repositories.Contracts
{
    public interface IUserRepository
    {
        void Register(ApplicationUser user, string password);
        ApplicationUser Get(string email, string password);
    }
}
