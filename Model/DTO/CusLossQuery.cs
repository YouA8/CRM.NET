namespace Model.DTO
{
    public class CusLossQuery : BaseQuery
    {
        public int? CusId { get; set; }
        public string CusName { get; set; }
        public int? State { get; set; }
    }
}
