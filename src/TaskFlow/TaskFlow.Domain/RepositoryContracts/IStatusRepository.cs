using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.RepositoryContracts
{
    public interface IStatusRepository : IRepositoryBase<Status, Guid>
    {
        (IList<Status> data, int total, int totalDisplay) GetPagedStatus(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
