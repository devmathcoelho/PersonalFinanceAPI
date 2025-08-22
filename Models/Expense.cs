namespace PersonalFinanceAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public string Category { get; set; }
        public string Date { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");

        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User User { get; set; } = null!;

    }
}
