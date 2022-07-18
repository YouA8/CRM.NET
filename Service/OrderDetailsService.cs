using AutoMapper;
using IRepository;
using IService;
using Model;

namespace Service
{
    public class OrderDetailsService : BaseService<OrderDetail>, IOrderDetailsService
    {
        private readonly IOrderDetailsReposiory _orderDetailsRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IOrderDetailsReposiory orderDetailsRepository, IMapper mapper)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _mapper = mapper;
        }
    }
}
