using IRepository;
using IService;
using Model;

namespace Service
{
    public class BaseService<T> : IBaseService<T> where T : class 
    {
        public BaseService()
        {
        }
    }
}
