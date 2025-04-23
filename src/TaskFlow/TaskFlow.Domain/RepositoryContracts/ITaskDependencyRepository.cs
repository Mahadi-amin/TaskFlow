using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.RepositoryContracts
{
    public interface ITaskDependencyRepository : IRepositoryBase<TaskDependency, Guid>
    {
    }
}
