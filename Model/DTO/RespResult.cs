namespace Model
{
    public class RespResult<T>
    {
        public T Data { get; set; }

        public int Code{ get; set; }
        public string Message { get; set; }

        public RespResult(T data, int code = (int)RespCode.Error, string message = "系统错误，请重试！")
        {
            Data = data;
            Code = code;
            Message = message;
        }
    }

    /// <summary>
    /// 响应Code
    /// </summary>
    public enum RespCode : int
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 操作失败
        /// </summary>
        Fail = 0,

        /// <summary>
        /// 系统错误
        /// </summary>
        Error = -1
    }
}
