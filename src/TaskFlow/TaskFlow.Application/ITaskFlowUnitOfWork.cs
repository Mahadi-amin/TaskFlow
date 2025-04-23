using TaskFlow.Domain;
using TaskFlow.Domain.RepositoryContracts;

namespace TaskFlow.Application
{
    public interface ITaskFlowUnitOfWork : IUnitOfWork
    {
        IStatusRepository StatusRepository { get; }
        ITaskItemRepository TaskItemRepository { get; }
        ITaskDependencyRepository TaskDependencyRepository { get; }
    }
}
