﻿using TaskFlow.Domain.Dtos;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.RepositoryContracts
{
    public interface ITaskItemRepository : IRepositoryBase<TaskItem, Guid>
    {
        Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTaskItemsAsync(int pageIndex, int pageSize, TaskItemDto search, string? order);
        Task<TaskItem> GetTaskWithPrerequisites(Guid id);
        Task<int> NearDueDateTaskAsync();
        Task<int> GetTaskCountByStatusAsync(string statusName);
        Task<List<TaskItem>> GetPendingTaskAsync();
        Task<List<TaskItem>> GetInProgressTaskAsync();
        Task<List<TaskItem>> GetTaskByStatusAsync();
    }
}
