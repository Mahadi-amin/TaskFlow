using System.Text.Json;
using TaskFlow.Domain.Entities;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.Data.SeedingMethod
{
    public class SeedTaskContextEntityFromJson
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Statuses.Any())
            {
                var statusData = File.ReadAllText("..\\TaskFlow.Infrastructure\\Data\\SeedData\\status.json");
                var status = JsonSerializer.Deserialize<List<Status>>(statusData);
                context.Statuses.AddRange(status);
            }

            if (!context.TaskItems.Any())
            {
                var taskItemsJson = File.ReadAllText("..\\TaskFlow.Infrastructure\\Data\\SeedData\\taskitem.json");
                var tasks = JsonSerializer.Deserialize<List<TaskItem>>(taskItemsJson);

                foreach (var task in tasks)
                {
                    task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);
                }
                context.TaskItems.AddRange(tasks);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }

    }
}
