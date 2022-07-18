using IRepository;
using Model;

namespace Repository
{
    public class OrderDetailsReposiory : BaseRepository<OrderDetail>, IOrderDetailsReposiory
    {
        public OrderDetailsReposiory(CRMDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
