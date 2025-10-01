namespace PersonalFinanceAPI.Dtos
{
    public class BillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public string Date { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");
        public string DueDate { get; set; }
        public bool IsPaid { get; set; }

        public int UserId { get; set; }
    }
}
