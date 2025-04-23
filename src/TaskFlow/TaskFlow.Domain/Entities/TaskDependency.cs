namespace TaskFlow.Domain.Entities
{
    public class TaskDependency : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        public Guid PrerequisiteTaskId { get; set; }
        public TaskItem? PrerequisiteTask { get; set; }
    }
}
