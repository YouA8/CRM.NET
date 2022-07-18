using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public IQueryable<User> GetUserByParams(UserQuery query);
    }
}
