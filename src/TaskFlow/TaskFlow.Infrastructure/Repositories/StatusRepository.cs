using TaskFlow.Domain.Entities;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class StatusRepository : Repository<Status, Guid>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
