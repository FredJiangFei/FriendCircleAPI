using System.Threading.Tasks;
using FriendCircle.Data;

namespace FriendCircle.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
    }
}