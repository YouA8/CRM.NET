namespace Model.DTO
{
    public class CustomerQuery : BaseQuery
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        public string Level { get; set; }
    }
}
