using TaskFlow.Application;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.UnitOfWorks
{
    public class TaskFlowUnitOfWork : UnitOfWork, ITaskFlowUnitOfWork
    {
        public TaskFlowUnitOfWork(
            ApplicationDbContext dbContext,
            IStatusRepository statusRepository
            ) : base(dbContext)
        {
            StatusRepository = statusRepository;
        }

        public IStatusRepository StatusRepository { get; private set; }
    }
}
