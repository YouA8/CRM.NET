using Model.DTO;

namespace Model
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public RoleDTO Role { get; set; }

        public UserDTO()
        {
        }
    }
}
