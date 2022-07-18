using System;

namespace Model.DTO
{
    public class CusServerDTO
    {
        public int? Id { get; set; }
        public string ServerType { get; set; }
        public string Overview { get; set; }
        public string Customer { get; set; }
        public string Creator { get; set; }
        public string Request { get; set; }
        public string Assigner { get; set; }
        public DateTime? AssignTime { get; set; }
        public string ServiceHandle { get; set; }
        public string ServiceHandler { get; set; }
        public string ServiceHandleResult { get; set; }
        public DateTime? ServiceHandleTime { get; set; }
        public string Myd { get; set; }
        public int State { get; set; }

        public CusServerDTO()
        {
        }
    }
}
