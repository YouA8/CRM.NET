using AutoMapper;
using IRepository;
using IService;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class CusServerService : BaseService<CusServer>, ICusServerService
    {
        private readonly ICusServerRepository _cusServerRepository;
        private readonly IMapper _mapper;

        public CusServerService(ICusServerRepository cusServerRepository, IMapper mapper)
        {
            _cusServerRepository = cusServerRepository;
            _mapper = mapper;
        }

        public RespResult<object> GetCusServerByParam(CusServerQuery query)
        {
            var cus = _cusServerRepository.GetCusServerByParam(query).ToList();
            var total = cus.Count;
            var temp = cus.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            var list = _mapper.Map<List<CusServer>, List<CusServerDTO>>(temp);
            if(list != null && list.Count > 0)
            {
                return new RespResult<object>(new { list, total }, (int)RespCode.Success, "客户服务列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> AddCusServer(CusServerDTO cusServerDTO)
        {
            // 1.值校验
            var cus = _cusServerRepository.DbContext.Customer.Where(c => c.Name == cusServerDTO.Customer && c.IsValid == 1).FirstOrDefault();
            if (cus != null && !string.IsNullOrEmpty(cusServerDTO.Customer) && !string.IsNullOrEmpty(cusServerDTO.Request))
            {
                // 2.值转换
                var cuss = _mapper.Map<CusServerDTO, CusServer>(cusServerDTO);
                // 3.默认值设置
                cuss.State = 1;
                cuss.CreateTime = DateTime.Now;
                cuss.UpdateTime = DateTime.Now;
                cuss.IsValid = 1;
                // 4.添加
                _cusServerRepository.Add(cuss);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "添加失败！");
        }

        public RespResult<object> UpdateCusServerState(int id, CusServerDTO cusServerDTO)
        {
            var cuss = _cusServerRepository.Where(c => c.Id == id && c.IsValid == 1).FirstOrDefault();
            if (cuss != null && cusServerDTO.State < 5)
            {
                // 服务分配
                if (cusServerDTO.State == 2)
                {
                    var cus = _cusServerRepository.DbContext.User.Where(c => c.Name == cusServerDTO.Assigner).FirstOrDefault();
                    if (cus != null)
                    {
                        cusServerDTO.AssignTime = DateTime.Now; 
                    }
                    else
                    {
                        return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
                    }
                }
                // 服务处理
                else if (cusServerDTO.State == 3)
                {
                    if (!string.IsNullOrEmpty(cusServerDTO.ServiceHandle))
                    {
                        cusServerDTO.ServiceHandleTime = DateTime.Now;
                    }
                    else
                    {
                        return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
                    }
                }
                // 服务反馈
                else if (cusServerDTO.State == 4)
                {
                    if (!string.IsNullOrEmpty(cusServerDTO.ServiceHandleResult) && !string.IsNullOrEmpty(cusServerDTO.Myd))
                    {
                        cusServerDTO.State = 5;
                    }
                    else
                    {
                        return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
                    }
                }
                var temp = _mapper.Map(cusServerDTO, cuss);
                temp.UpdateTime = DateTime.Now;
                _cusServerRepository.Update(temp);
                return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
        }

        public RespResult<object> DeleteCusServer(int[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    var cusServer = _cusServerRepository.Where(cuss => cuss.Id == id && cuss.IsValid == 1).FirstOrDefault();
                    cusServer.IsValid = 0;
                    _cusServerRepository.Update(cusServer);
                }
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<object> GetServerType()
        {
            var list = new List<object>()
            {
                new {
                    Id = 1, 
                    ServerType = "咨询"
                },
                new
                {
                    Id = 2,
                    ServerType = "投诉"
                },
                new
                {
                    Id = 3,
                    ServerType ="建议"
                }
            };
            return new RespResult<object>(list, (int)RespCode.Success, "服务类型！");
        }

        public RespResult<object> ServerMake()
        {
            var server = _cusServerRepository.ServerMake().ToList(); 
            if (server != null && server.Count >0)
            {
                return new RespResult<object>(server, (int)RespCode.Success, "服务构成！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }
    }
}
