using TaskFlow.Application.ServicesInterface;
using TaskFlow.Domain.Dtos;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services
{
    public class TaskItemService(ITaskFlowUnitOfWork taskUnitOfWork) : ITaskItemService
    {
        private readonly ITaskFlowUnitOfWork _taskUnitOfWork = taskUnitOfWork;

        public async Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTasksAsync(int pageIndex, int pageSize, TaskItemDto search, string? order)
        {
            return await _taskUnitOfWork.TaskItemRepository.GetAllTaskItemsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<TaskItem>> GetTaskListAsync()
        {
            return await _taskUnitOfWork.TaskItemRepository.GetAllAsync();
        }

        public async Task<IList<Status>> GetStatusListAsync()
        {
            return await _taskUnitOfWork.StatusRepository.GetAllAsync();
        }

        public async Task CreateNewTaskAsync(TaskItem taskItem)
        {
            await _taskUnitOfWork.TaskItemRepository.AddAsync(taskItem);
            await _taskUnitOfWork.SaveAsync();
        }

        public async Task CreateNewDependencyAsync(Guid taskId, List<Guid> prerequisiteIds)
        {
            if (taskId != Guid.Empty && prerequisiteIds != null)
            {
                foreach (var prerequisiteId in prerequisiteIds)
                {
                    var taskDepenedency = new TaskDependency()
                    {
                        Id = Guid.NewGuid(),
                        TaskItemId = taskId,
                        PrerequisiteTaskId = prerequisiteId
                    };
                    await _taskUnitOfWork.TaskDependencyRepository.AddAsync(taskDepenedency);
                }
            }
            await _taskUnitOfWork.SaveAsync();
        }

        public Task<int> GetUpcomingDueTaskCountAsync()
        {
            return _taskUnitOfWork.TaskItemRepository.NearDueDateTaskAsync();
        }
    }

}
