using AutoMapper;
using IRepository;
using IService;
using Model;
using Model.DTO;
using System;
using System.Linq;

namespace Service
{
    public class CusLossService : BaseService<CusLoss>, ICusLossService
    {
        private readonly ICusLossRepository _cusLossRepository;
        private readonly IMapper _mapper;

        public CusLossService(ICusLossRepository cusLossRepository, IMapper mapper)
        {
            _cusLossRepository = cusLossRepository;
            _mapper = mapper;
        }

        public RespResult<object> GetCusLossByParams(CusLossQuery query)
        {
            var cusLoss = _cusLossRepository.GetCusLossByParams(query).ToList();
            var total = cusLoss.Count;
            var list = cusLoss.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { list, total }, (int)RespCode.Success, "客户流失列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> UpdateCusLossState(int id, string lossReason)
        {
            var cusloss = _cusLossRepository.Where(c => c.IsValid == 1 && c.Id == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(lossReason) && cusloss != null) 
            {
                cusloss.LossReason = lossReason;
                cusloss.State = 1;
                cusloss.ConfirmLossTime = DateTime.Now;
                cusloss.UpdateTime = DateTime.Now;
                _cusLossRepository.Update(cusloss);
                return new RespResult<object>(null, (int)RespCode.Success, "流失状态修改成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "流失状态修改失败！");
        }
    }
}
