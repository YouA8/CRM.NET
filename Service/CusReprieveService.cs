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
    public class CusReprieveService : BaseService<CusReprieve>, ICusReprieveService
    {
        private readonly ICusReprieveRepository _cusReprieveRepository;
        private readonly IMapper _mapper;

        public CusReprieveService(ICusReprieveRepository cusReprieveRepository, IMapper mapper)
        {
            _cusReprieveRepository = cusReprieveRepository;
            _mapper = mapper;
        }

        public RespResult<object> AddCusReprieve(CusReprieveDTO cusReprieveDTO)
        {
            // 1.值校验
            if (ValueVaild(cusReprieveDTO))
            {
                // 2. 值转换
                var cusreprieve = _mapper.Map<CusReprieveDTO, CusReprieve>(cusReprieveDTO);
                // 3.默认值设置
                cusreprieve.IsValid = 1;
                cusreprieve.CreateTime = DateTime.Now;
                cusreprieve.UpdateTime = DateTime.Now;
                _cusReprieveRepository.Add(cusreprieve);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "数据有误！");
        }

        public RespResult<object> DeleteCusReprieve(int id)
        {
            var cusr = _cusReprieveRepository.Where(l => l.Id == id && l.IsValid == 1).FirstOrDefault();
            if(cusr != null)
            {
                _cusReprieveRepository.Update(cusr);
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<object> GetCusReprieveByParam(CusReprieveQuery query)
        {
            var cusreprieve = _cusReprieveRepository.GetCusReprieveByParam(query).ToList();
            var total = cusreprieve.Count;
            var temp = cusreprieve.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            var list = _mapper.Map<List<CusReprieve>, List<CusReprieveDTO>>(temp);
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { list, total }, (int)RespCode.Success, "客户暂留列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> UpdateCusReprieve(int id, CusReprieveDTO cusReprieveDTO)
        {
            if (ValueVaild(cusReprieveDTO))
            {
                var cusr = _cusReprieveRepository.Where(l => l.Id == id && l.IsValid == 1).FirstOrDefault();
                if(cusr != null)
                {
                    cusr = _mapper.Map(cusReprieveDTO, cusr);
                    cusr.UpdateTime = DateTime.Now;
                    _cusReprieveRepository.Update(cusr);
                    return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
                }
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
        }

        /// <summary>
        /// 数值校验
        /// </summary>
        /// <param name="cusReprieveDTO"></param>
        /// <returns></returns>
        private bool ValueVaild(CusReprieveDTO cusReprieveDTO)
        {
            //流失客户是否存在
            var cus = _cusReprieveRepository.DbContext.CusLoss.Where(c => c.Id == cusReprieveDTO.LossId && c.IsValid == 1).FirstOrDefault();
            // 措施不能为空
            if (cus != null && !string.IsNullOrEmpty(cusReprieveDTO.Measure))
            {
                return true;
            }
            return false;
        }
    }
}
