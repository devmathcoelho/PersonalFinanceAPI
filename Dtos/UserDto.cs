using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CategoryDto> Categories { get; set; } = new();
        public List<ExpenseDto> Expenses { get; set; } = new();
        public List<BillDto> Bills { get; set; } = new();

        public float TotalRevenue { get; set; } = 0;
        public float TotalExpense { get; set; } = 0;

        public float TotalBalance => TotalRevenue - TotalExpense;
    }
}