using TaskFlow.Domain;
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

        public (IList<Status> data, int total, int totalDisplay) GetPagedStatus(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
                return GetDynamic(null, order, null, pageIndex, pageSize, true);
            else
                return GetDynamic(x => x.StatusName.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }
    }
}
