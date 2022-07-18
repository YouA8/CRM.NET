namespace Model.DTO
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Token { get; set; }
        public string VaildCode { get; set; } = "";

        public UserLogin()
        {
        }
    }
}
