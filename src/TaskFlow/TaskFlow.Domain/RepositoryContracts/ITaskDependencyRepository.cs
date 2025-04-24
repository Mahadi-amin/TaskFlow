using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.RepositoryContracts
{
    public interface ITaskDependencyRepository : IRepositoryBase<TaskDependency, Guid>
    {
        Task<IList<TaskDependency>> GetDependenciesAsync(Guid taskId);
    }
}
