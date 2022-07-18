namespace Model.DTO
{
    public class SaleChanceQuery : BaseQuery
    {
        public string CustomerName { get; set; }
        public string CreatorName { get; set; }
        public int? State { get; set; }
        public int? DevResult { get; set; }
        public string AssignorName { get; set; }

        public SaleChanceQuery()
        {
        }
    }
}
