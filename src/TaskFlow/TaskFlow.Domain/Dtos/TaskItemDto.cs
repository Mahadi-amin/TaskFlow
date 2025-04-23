namespace TaskFlow.Domain.Dtos
{
    public class TaskItemDto
    {
        public string? StatusId { get; set; }
        public int? Priority { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
