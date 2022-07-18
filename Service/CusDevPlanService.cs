using AutoMapper;
using IRepository;
using IService;
using Model;
using Model.DTO;
using System;
using System.Linq;

namespace Service
{
    public class CusDevPlanService : BaseService<CusDevPlan>, ICusDevPlanService
    {
        private readonly ICusDevPlanRepository _cusDevPlanRepository;
        private readonly IMapper _mapper;

        public CusDevPlanService(ICusDevPlanRepository cusDevPlanRepository, IMapper mapper)
        {
            _cusDevPlanRepository = cusDevPlanRepository;
            _mapper = mapper; 
        }

        public RespResult<object> GetCusDevPlanByParams(CusDevPlanQuery query)
        {
            // ef core 对sql优化问题 sqlserver2008不支持 只能先查到内存 后分页
            var temp = _cusDevPlanRepository.Where(q => q.SaleChanceId == query.SaleCanceId && q.IsValid == 1).ToList();
            var total = temp.Count();
            var list = temp.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { total, list }, (int)RespCode.Success, "客户开发计划列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> AddCusDevPlan(CusDevPlanDTO cusDevPlanDTO)
        {
            // 1.参数校验
            if (ValueVaild(cusDevPlanDTO))
            {
                // 2.映射值
                var cusdevplan = _mapper.Map<CusDevPlanDTO, CusDevPlan>(cusDevPlanDTO);
                // 3.设定默认值
                cusdevplan.CreateTime = DateTime.Now;
                cusdevplan.UpdateTime = DateTime.Now;
                cusdevplan.IsValid = 1;
                _cusDevPlanRepository.Add(cusdevplan);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "添加失败！");
        }

        public RespResult<object> DeleteCusDevPlan(int[] ids)
        {
            if (ids != null && ids.Length >= 0)
            {
                foreach (var id in ids)
                {
                    var cusDevPlan = _cusDevPlanRepository.GetById(id);
                    cusDevPlan.IsValid = 0;
                    _cusDevPlanRepository.Update(cusDevPlan);
                }
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<object> UpdateCusDevPlan(CusDevPlanDTO cusDevPlanDTO,int id)
        {
            // 1.数据校验
            var cusdevplan = _cusDevPlanRepository.GetById(id); 
            if (cusdevplan != null)
            {
                if (ValueVaild(cusDevPlanDTO))
                {
                    // 2.值映射
                    var cusdecplan = _mapper.Map(cusDevPlanDTO, cusdevplan);
                    // 3.更改
                    cusdecplan.UpdateTime = DateTime.Now;
                    cusdevplan.Id = id;
                    _cusDevPlanRepository.Update(cusdecplan);
                    return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
                }
                return new RespResult<object>(null, (int)RespCode.Fail, "数据有误！");
            }
            return new RespResult<object>(null,(int)RespCode.Fail,"数据不存在！") ;
        }

        public RespResult<object> Details(int id)
        {
            var cus = _cusDevPlanRepository.Where(c => c.IsValid == 1 && c.Id == id).FirstOrDefault();
            var res = _mapper.Map<CusDevPlan, CusDevPlanDTO>(cus);
            return new RespResult<object>(res, (int)RespCode.Success, "营销机会详情");
        }

        /// <summary>
        /// 对数据进行校验
        /// </summary>
        /// <param name="cusDevPlanDTO"></param>
        /// <returns></returns>
        private bool ValueVaild(CusDevPlanDTO cusDevPlanDTO)
        {
            if (!string.IsNullOrEmpty(cusDevPlanDTO.PlanCantext))
            {
                var cus = _cusDevPlanRepository.DbContext.SaleChance.Where(c => c.Id == cusDevPlanDTO.SaleChanceId && c.IsValid == 1).FirstOrDefault();
                if(cus != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
