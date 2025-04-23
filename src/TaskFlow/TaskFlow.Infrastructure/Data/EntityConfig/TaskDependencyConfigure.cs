using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.EntityConfig
{
    public class TaskDependencyConfigure : IEntityTypeConfiguration<TaskDependency>
    {
        public void Configure(EntityTypeBuilder<TaskDependency> builder)
        {
            builder.HasKey(x => new { x.TaskItemId, x.PrerequisiteTaskId });

            builder.HasOne(td => td.TaskItem)
                    .WithMany(t => t.PrerequisiteLinks)
                    .HasForeignKey(td => td.TaskItemId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(td => td.PrerequisiteTask)
                    .WithMany(t => t.DependentLinks)
                    .HasForeignKey(td => td.PrerequisiteTaskId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
