namespace Model.DTO
{
    public class CusServerQuery : BaseQuery
    {
        public string Assigner { get; set; }
        public string Customer { set; get; }
        public string ServerType { get; set; }
        public int? State { get; set; }
    }
}
