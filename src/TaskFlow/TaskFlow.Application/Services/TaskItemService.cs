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

        public Task<TaskItem> GetTaskAsync(Guid id)
        {
            return _taskUnitOfWork.TaskItemRepository.GetTaskWithPrerequisites(id);

        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            await _taskUnitOfWork.TaskItemRepository.EditAsync(task);
            await _taskUnitOfWork.SaveAsync();
        }

        public async Task UpdateDependencyAsync(Guid taskId, List<Guid>? prerequisiteIds)
        {
            if (prerequisiteIds == null || prerequisiteIds.Count() <= 0)
            {
                return;
            }

            var existingDependencies = await _taskUnitOfWork.TaskDependencyRepository
                .GetDependenciesAsync(taskId);

            foreach (var dep in existingDependencies)
            {
                await _taskUnitOfWork.TaskDependencyRepository.RemoveAsync(dep);
            }

            foreach (var prerequisiteId in prerequisiteIds)
            {
                var taskDependency = new TaskDependency
                {
                    Id = Guid.NewGuid(),
                    TaskItemId = taskId,
                    PrerequisiteTaskId = prerequisiteId
                };

                await _taskUnitOfWork.TaskDependencyRepository.AddAsync(taskDependency);
            }

            await _taskUnitOfWork.SaveAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskUnitOfWork.TaskItemRepository.RemoveAsync(id);

            var existingDependencies = await _taskUnitOfWork.TaskDependencyRepository
                .GetDependenciesAsync(id);

            foreach (var dep in existingDependencies)
            {
                await _taskUnitOfWork.TaskDependencyRepository.RemoveAsync(dep);
            }

            await _taskUnitOfWork.SaveAsync();
        }

        public async Task<int> GetUpcomingDueTaskCountAsync()
        {
            return await _taskUnitOfWork.TaskItemRepository.NearDueDateTaskAsync();
        }

        public async Task<int> GetAllTaskCountByStatusAsync(string taskStatusNames)
        {
            return await _taskUnitOfWork.TaskItemRepository.GetTaskCountByStatusAsync(taskStatusNames);
        }

        public async Task<List<TaskItem>> GetAllPendingTaskAsync()
        {
            return await _taskUnitOfWork.TaskItemRepository.GetPendingTaskAsync();
        }

        public async Task<List<TaskItem>> GetAllInProgressTaskAsync()
        {
            return await _taskUnitOfWork.TaskItemRepository.GetInProgressTaskAsync();
        }

        public async Task<List<TaskItem>> GetTaskByStatusAsync()
        {
            return await _taskUnitOfWork.TaskItemRepository.GetTaskByStatusAsync(); 
        }
    }

}
