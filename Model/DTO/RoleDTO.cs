using System.Collections.Generic;

namespace Model.DTO
{
    public class RoleDTO
    {
        public int? Id { get; set; }
        public string RoleName { get; set; }
        public string Remarke { get; set; }
        public List<PermissionDTO> Permission { get; set; } 

        public RoleDTO()
        {
        }
    }
}
