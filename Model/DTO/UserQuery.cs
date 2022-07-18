using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UserQuery : BaseQuery
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public UserQuery()
        {
        }
    }
}
