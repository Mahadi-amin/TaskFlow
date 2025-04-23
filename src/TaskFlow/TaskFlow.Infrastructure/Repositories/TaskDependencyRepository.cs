using TaskFlow.Domain.Entities;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskDependencyRepository : Repository<TaskDependency, Guid>, ITaskDependencyRepository
    {
        public TaskDependencyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
