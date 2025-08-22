namespace PersonalFinanceAPI.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string ExpireDate { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");

        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User User { get; set; } = null!;

    }
}
