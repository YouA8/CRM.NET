using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CusDevPlanQuery : BaseQuery
    {
        public int SaleCanceId { get; set; }
        public CusDevPlanQuery()
        {
        }
    }
}
