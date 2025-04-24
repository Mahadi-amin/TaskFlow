using TaskFlow.Domain.Dtos;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.ServicesInterface
{
    public interface ITaskItemService
    {
        Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTasksAsync(int pageIndex, int pageSize, TaskItemDto search, string? order);
        Task<IList<TaskItem>> GetTaskListAsync();
        Task<IList<Status>> GetStatusListAsync();
        Task CreateNewTaskAsync(TaskItem taskItem);
        Task CreateNewDependencyAsync(Guid taskId, List<Guid> prerequisiteIds);
        Task<TaskItem> GetTaskAsync(Guid id);
        Task UpdateTaskAsync(TaskItem task);
        Task UpdateDependencyAsync(Guid Id, List<Guid> prerequisiteIds);
        Task DeleteTaskAsync(Guid id);
        Task<int> GetUpcomingDueTaskCountAsync();
        Task<int> GetAllTaskCountByStatusAsync(string taskStatusNames);

    }
}
