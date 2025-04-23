namespace TaskFlow.Domain.Entities
{
    public class TaskItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid? StatusId { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public List<TaskDependency>? PrerequisiteLinks { get; set; }
        public List<TaskDependency>? DependentLinks { get; set; }
    }
}
