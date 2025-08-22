namespace PersonalFinanceAPI.Models
{
    public class Revenue
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Value { get; set; }
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");

        public int UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User User { get; set; } = null!;
    }
}
