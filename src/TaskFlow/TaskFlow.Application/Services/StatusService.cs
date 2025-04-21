using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly ITaskFlowUnitOfWork _taskFlowUnitOfWork;

        public StatusService(ITaskFlowUnitOfWork taskFlowUnitOfWork)
        {
            _taskFlowUnitOfWork = taskFlowUnitOfWork;
        }

        public IList<Status> GetAllStatus()
        {
            return _taskFlowUnitOfWork.StatusRepository.GetAll();
        }
    }
}
