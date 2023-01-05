using InstagramSystem.Data;
using InstagramSystem.DTOs;
using InstagramSystem.Entities;

namespace InstagramSystem.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByUserName(string UserName);

    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<User> GetUserByUserName(string UserName)
        {
            return context.Users.Where(user => user.UserName == UserName).FirstOrDefault();
        }

    }
}
