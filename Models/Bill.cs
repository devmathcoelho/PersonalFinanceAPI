namespace PersonalFinanceAPI.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Value { get; set; }
        public string DueDate { get; set; } = string.Empty;
        public bool IsPaid { get; set; } = false;
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");

        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User User { get; set; } = null!;

    }
}
