namespace PersonalFinanceAPI.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Month { get; set; }
        public int UserId { get; set; }

    }
}
