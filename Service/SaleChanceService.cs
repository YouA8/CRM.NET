using AutoMapper;
using Common;
using IRepository;
using IService;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class SaleChanceService : BaseService<SaleChance>, ISaleChanceService
    {
        private readonly ISaleChanceRepository _saleChanceRepository;
        private readonly IMapper _mapper;

        public SaleChanceService(ISaleChanceRepository saleChanceRepository, IMapper mapper)
        {
            _saleChanceRepository = saleChanceRepository;
            _mapper = mapper;
        }

        public RespResult<object> GetSaleChanceByParams(SaleChanceQuery query)
        {
            // ef core 对sql优化问题 sqlserver2008不支持 只能先查到内存 后分页
            // 1.获取多条件查询结果
            var salechance = _saleChanceRepository.GetSaleChanceByParams(query).ToList();
            // 2.分页查询
            var total = salechance.Count;
            var temp = salechance.Skip((query.Page - 1 * query.PageSize)).Take(query.PageSize).ToList();
            var list = _mapper.Map<List<SaleChance>, List<SaleChanceDTO>>(temp);
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { total, list }, (int)RespCode.Success, "营销机会列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> AddSaleChance(SaleChanceDTO saleChanceDTO)
        {
            // 1.参数校验
            if (ValueVaild(saleChanceDTO))
            {
                // 2.使用AutoMapper设值 -- 前端传来
                var salechance = _mapper.Map<SaleChanceDTO, SaleChance>(saleChanceDTO);
                // 3.设置默认值
                salechance.CreateTime = DateTime.Now;
                salechance.UpdateTime = DateTime.Now;
                salechance.IsValid = 1;
                // 4.业务值 -- 修改 限制设值
                var user = _saleChanceRepository.DbContext.User.Where(u => u.Name == salechance.AssignorName && u.IsValid == 1).FirstOrDefault();
                if (user == null)
                {
                    salechance.AssignTime = null;
                    salechance.State = 0;
                    salechance.DevResult = 0;
                }
                else
                {
                    salechance.AssignTime = DateTime.Now;
                    salechance.State = 1;
                    salechance.DevResult = 1;
                }
                // 5.添加
                _saleChanceRepository.Add(salechance);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "客户信息（名称，联系人，电话）数据有误！");
        }

        public RespResult<object> UpdateSaleChance(SaleChanceDTO saleChanceDTO, int id)
        {
            // 1.校验数据是否存在 正确
            var saleChance = _saleChanceRepository.Where(s=>s.IsValid == 1 && s.Id == id).FirstOrDefault();
            if (saleChance != null)
            {
                if (ValueVaild(saleChanceDTO))
                {
                    // 2.数据映射
                    saleChance = _mapper.Map(saleChanceDTO,saleChance);
                    // 3.判断业务 设值
                    var user = _saleChanceRepository.DbContext.User.Where(u => saleChance.AssignorName == u.Name && u.IsValid == 1).FirstOrDefault();
                    if (user == null)
                    {
                        saleChance.AssignorName = null;
                        saleChance.AssignTime = null;
                        saleChance.State = 0;
                        saleChance.DevResult = 0;
                    }
                    else
                    {
                        saleChance.AssignorName = user.Name;
                        saleChance.AssignTime = DateTime.Now;
                        saleChance.State = 1;
                        saleChance.DevResult = 1;
                    }
                    // 4.更改
                    saleChance.UpdateTime = DateTime.Now;
                    saleChance.Id = id;
                    _saleChanceRepository.Update(saleChance);
                    return new RespResult<object>(null, (int)RespCode.Success, "更改成功！"); 
                }
                return new RespResult<object>(null, (int)RespCode.Fail, "数据有误！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "数据不存在！"); 
        }

        public RespResult<object> UpdateSaleChanceDevResult(SaleChanceDTO saleChanceDTO, int id)
        {
            var saleChance = _saleChanceRepository.Where(s => s.IsValid == 1 && s.Id == id).FirstOrDefault();
            if (saleChance != null)
            {
                if (ValueVaild(saleChanceDTO))
                {
                    saleChance = _mapper.Map(saleChanceDTO, saleChance);
                    saleChance.UpdateTime = DateTime.Now;
                    saleChance.Id = id;
                    _saleChanceRepository.Update(saleChance);
                    return new RespResult<object>(null, (int)RespCode.Success, "更改成功！");
                }
                return new RespResult<object>(null, (int)RespCode.Fail, "数据有误！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "数据不存在！");
        }

        public RespResult<object> DeleteSaleChance(int[] ids)
        {
            if (ids != null && ids.Length >= 0)
            {
                foreach(var id in ids)
                {
                    var saleChance = _saleChanceRepository.GetById(id);
                    saleChance.IsValid = 0;
                    _saleChanceRepository.Update(saleChance);
                }
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<SaleChanceDTO> Details(int id)
        {
            var saleChance = _saleChanceRepository.Where(s => s.IsValid == 1 && s.Id == id).FirstOrDefault();
            var res = _mapper.Map<SaleChance, SaleChanceDTO>(saleChance);
            return new RespResult<SaleChanceDTO>(res, (int)RespCode.Success, "营销机会详情");
        }

        private bool ValueVaild(SaleChanceDTO saleChanceDTO)
        {
            if (!string.IsNullOrEmpty(saleChanceDTO.CustomerName) && !string.IsNullOrEmpty(saleChanceDTO.Phone) && IsValueUtil.IsPhone(saleChanceDTO.Phone) && !string.IsNullOrEmpty(saleChanceDTO.Contact))
            {
                var customer = _saleChanceRepository.DbContext.Customer.Where(c => saleChanceDTO.CustomerName == c.Name && c.IsValid == 1).FirstOrDefault();
                if (customer != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
