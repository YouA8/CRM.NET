using System;

namespace Model
{
    public class CusServer
    {
        public int Id { get; set; }
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
        public int State { get; set; }              // 服务状态  1服务创建 2服务分配 3服务处理 4服务反馈 5服务归档
        public int IsValid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public CusServer()
        {
        }

        public CusServer(int id, string serverType, string overview, string customer, string creator, string request, string assigner, DateTime? assignTime, string serviceHandle, string serviceHandler, string serviceHandleResult, DateTime? serviceHandleTime, string myd, int state, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            ServerType = serverType;
            Overview = overview;
            Customer = customer;
            Creator = creator;
            Request = request;
            Assigner = assigner;
            AssignTime = assignTime;
            ServiceHandle = serviceHandle;
            ServiceHandler = serviceHandler;
            ServiceHandleResult = serviceHandleResult;
            ServiceHandleTime = serviceHandleTime;
            Myd = myd;
            State = state;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
