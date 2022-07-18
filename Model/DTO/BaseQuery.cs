namespace Model.DTO
{
    /// <summary>
    /// 基础分页
    /// </summary>
    public class BaseQuery
    {
        public int PageSize { get; set; }
        public int Page { get; set; } = 1;
    }
}
