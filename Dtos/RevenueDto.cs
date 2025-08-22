namespace PersonalFinanceAPI.Dtos
{
    public class RevenueDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Value { get; set; }
        public string CreatedAt { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");

        public int UserId { get; set; }

    }
}
