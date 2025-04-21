using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services
{
    public interface IStatusService
    {
        IList<Status> GetAllStatus();
    }
}
