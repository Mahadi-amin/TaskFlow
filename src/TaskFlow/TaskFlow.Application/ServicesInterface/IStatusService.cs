using TaskFlow.Domain;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.ServicesInterface
{
    public interface IStatusService
    {
        IList<Status> GetAllStatus();
        (IList<Status> data, int total, int totalDisplay) GetStatus(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
