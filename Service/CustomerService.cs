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
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public RespResult<object> AddCustomer(CustomerDTO customerDTO)
        {
            // 1.数据校验
            if (ValueVaild(customerDTO, -1))
            {
                // 2.值映射
                var customer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
                // 3.默认设置
                customer.CreateTime = DateTime.Now;
                customer.UpdateTime = DateTime.Now;
                customer.IsValid = 1;
                customer.State = 1;
                // 4.添加
                _customerRepository.Add(customer);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "添加失败!");
        }

        public RespResult<object> DeleteCustomer(int[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    var customer = _customerRepository.Where(cus => cus.Id == id && cus.IsValid == 1).FirstOrDefault();
                    customer.IsValid = 0;
                    _customerRepository.Update(customer);    
                }
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<object> GetCustomerByParam(CustomerQuery query)
        {
            // ef core 对sql优化问题 sqlserver2008不支持 只能先查到内存 后分页
            // 1.获取多条件查询结果
            var customer = _customerRepository.GetCustomerByParams(query).ToList();
            var total = customer.Count;
            // 2.分页
            var temp = customer.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            var list = _mapper.Map<List<Customer>, List<CustomerDTO>>(temp);
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { total, list }, (int)RespCode.Success, "客户列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> UpdateCustomer(CustomerDTO customerDTO, int id)
        {
            var customer = _customerRepository.Where(c => c.Id == id && c.IsValid == 1).FirstOrDefault();
            // 1.数据验证
            if (ValueVaild(customerDTO, customer.Id))
            {
                if (customer != null)
                {
                    // 2.值映射
                    var temp = _mapper.Map(customerDTO, customer);
                    // 3.默认值
                    temp.IsValid = 1;
                    temp.UpdateTime = DateTime.Now;
                    temp.Id = id;
                    // 4.修改
                    _customerRepository.Update(temp);
                    return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
                }
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
        }

        public RespResult<object> UpdateCusLossState()
        {
            //1.查找流失客户
            var clist = _customerRepository.GetLossCustomer().ToList();
            var lossids = new List<int>();
            if (clist != null && clist.Count > 0)
            {
                var lossclist = new List<CusLoss>();
                //1.遍历流失客户添加到流失列表 
                clist.ForEach(c =>
                {
                    var cusloss = new CusLoss();
                    cusloss.CusId = c.Id;
                    cusloss.IsValid = 1;
                    cusloss.CreateTime = DateTime.Now;
                    cusloss.UpdateTime = DateTime.Now;
                    cusloss.State = 0;  //客户暂缓流失
                    c.State = 0;
                    //2.查询客户最后订单
                    var cusOrder = _customerRepository.DbContext.CusOrder.Where(co => co.CusId == c.Id && co.IsValid == 1).OrderBy(co => co.OrderTime).LastOrDefault();
                    if (cusOrder != null)
                    {
                        cusloss.LastOrderTime = cusOrder.OrderTime;
                    }
                    lossclist.Add(cusloss);
                    lossids.Add(c.Id);
                });
                _customerRepository.DbContext.AddRange(lossclist);
                _customerRepository.DbContext.UpdateRange(clist);
                _customerRepository.SaveChanges();
                return new RespResult<object>(null, (int)RespCode.Success, "状态更新成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "更新状态失败！");
        }

        public RespResult<object> GetCusContributionByParam(CusContributionQuery query)
        {
            var cus = _customerRepository.GetCusContributionByParam(query).ToList();
            var total = cus.Count;
            var list = cus.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { list, total }, (int)RespCode.Success, "客户贡献列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> GetCusMake()
        {
            var cusm = _customerRepository.GetCusMake().ToList();
            if (cusm != null && cusm.Count > 0)
            {
                return new RespResult<object>(cusm, (int)RespCode.Success, "客户构成！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        private bool ValueVaild(CustomerDTO customerDTO,int id)
        {
            if (!string.IsNullOrEmpty(customerDTO.Name) && IsValueUtil.IsEmail(customerDTO.Email) && IsValueUtil.IsPhone(customerDTO.Phone))
            {
                var cus = _customerRepository.Where(c => c.Name == customerDTO.Name && c.IsValid ==1 && c.State == 1).FirstOrDefault();
                if (id == -1)
                {
                    if(cus == null)
                    {
                        return true;
                    }
                }
                else
                {
                    if(cus != null && cus.Id == id)
                    {
                        return true;
                    }
                    else if(cus == null){
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
