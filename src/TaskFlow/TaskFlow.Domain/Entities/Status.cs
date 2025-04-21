namespace TaskFlow.Domain.Entities
{
    public class Status : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public string? StatusDescription { get; set; } = string.Empty;
        public IList<TaskItem>? TaskItems { get; set; }
    }
}
