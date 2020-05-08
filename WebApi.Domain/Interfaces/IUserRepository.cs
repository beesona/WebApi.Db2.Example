using WebApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userId, CancellationToken ct = default);
    }
}