using IRepository;
using Model;
using Model.DTO;
using System.Linq;

namespace Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<User> GetUserByParams(UserQuery query)
        {
            var user = _dbContext.User.Where(u => u.IsValid == 1);
            if (!string.IsNullOrEmpty(query.UserName))
            {
                user = user.Where(u => u.UserName.Contains(query.UserName));
            }
            if (!string.IsNullOrEmpty(query.Email))
            {
                user = user.Where(u => u.Email.Contains(query.UserName));
            }
            if (!string.IsNullOrEmpty(query.Phone))
            {
                user = user.Where(u => u.Phone.Contains(query.Phone));
            }
            return user;
        }
    }
}
