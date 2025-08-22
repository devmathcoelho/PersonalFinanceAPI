namespace PersonalFinanceAPI.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public string Category { get; set; }
        public string Date { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");

        public int UserId { get; set; }
    }
}
