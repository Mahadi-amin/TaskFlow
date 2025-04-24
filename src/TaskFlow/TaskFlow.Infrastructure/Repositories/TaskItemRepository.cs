using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Dtos;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskItemRepository : Repository<TaskItem, Guid>, ITaskItemRepository
    {
        public TaskItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTaskItemsAsync(int pageIndex, int pageSize, TaskItemDto search, string? order)
        {
            var query = _dbSet.AsQueryable();

            var total = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(search.StatusId) && Guid.TryParse(search.StatusId, out var statusId))
            {
                query = query.Where(p => p.StatusId.Equals(statusId));
            }

            if (search.Priority != null)
            {
                query = query.Where(p => (int)p.Priority == search.Priority);
            }

            if (search.FromDate.HasValue)
            {
                query = query.Where(p => p.DueDate >= DateTime.SpecifyKind((DateTime)search.FromDate, DateTimeKind.Utc));
            }

            if (search.ToDate.HasValue)
            {
                query = query.Where(p => p.DueDate <= DateTime.SpecifyKind((DateTime)search.ToDate, DateTimeKind.Utc));
            }

            var (data, totalDisplay) = await GetDynamicDataAsync(query, order, x => x.Include(y => y.Status), pageIndex, pageSize, true);

            return (data, total, totalDisplay);
        }

        public async Task<TaskItem?> GetTaskWithPrerequisites(Guid id)
        {

            return await _dbSet
                            .Include(x => x.PrerequisiteLinks)
                            .Include(x => x.Status)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> NearDueDateTaskAsync()
        {
            var nowUtc = DateTime.UtcNow;
            var sevenDaysLaterUtc = nowUtc.AddDays(7);

            var upcomingTasksCount = await _dbSet
                .Where(t => t.DueDate > nowUtc && t.DueDate <= sevenDaysLaterUtc)
                .CountAsync();

            return upcomingTasksCount;
        }

        public async Task<int> GetPendingTaskAsync()
        {
            var taskCount = await _dbSet.CountAsync(t => t.Status.StatusName == "Pending");
            return taskCount;
        }

        public Task<int> GetInProgressTaskAsync()
        {
            var taskCount = _dbSet.CountAsync(t => t.Status.StatusName == "In-Progress");
            return taskCount;
        }

        public Task<int> GetCompletedTaskAsync()
        {
            var taskCount = _dbSet.CountAsync(t => t.Status.StatusName == "Completed");
            return taskCount;
        }
    }
}
