using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.EntityConfig
{
    public class TaskItemConfigure : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DueDate).IsRequired();
            builder.Property(t => t.Priority).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(500);

            builder.HasOne(x => x.Status)
                   .WithMany(y => y.TaskItems)
                   .HasForeignKey(z => z.StatusId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
