namespace PersonalFinanceAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CategoryDto> Categories { get; set; } = new();
        public List<ExpenseDto> Expenses { get; set; } = new();
        public List<RevenueDto> Revenues { get; set; } = new();
    }
}