namespace Model.DTO
{
    public class PwdChange
    {
        public string UserName { get; set; }
        public string OldUserPwd { get; set; }
        public string NewUserPwd { get; set; }

        public PwdChange()
        {
        }
    }
}
