namespace Domain.Dtos.EmailDto
{
    public class EmailResultDto
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
