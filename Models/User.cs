namespace PersonalFinanceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new();
        public List<Expense> Expenses { get; set; } = new();
        public List<Revenue> Revenues { get; set; } = new();
        public List<Bill> Bills { get; set; } = new();
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");
    }
}