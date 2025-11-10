namespace PersonalFinanceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserState { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new();
        public List<Expense> Expenses { get; set; } = new();
        public List<Bill> Bills { get; set; } = new();
        public float TotalRevenue { get; set; } = 0;
        public float TotalExpense { get; set; } = 0;
        public float TotalBalance { get; set; } = 0;
        public string CreatedAt { get; } = DateTime.UtcNow.ToString("dd-MM-yyyy");
    }
}