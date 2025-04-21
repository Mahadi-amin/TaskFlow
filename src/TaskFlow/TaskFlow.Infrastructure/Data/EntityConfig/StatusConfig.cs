using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.EntityConfig
{
    public class StatusConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.StatusName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.StatusDescription).HasMaxLength(100);
        }
    }
}
