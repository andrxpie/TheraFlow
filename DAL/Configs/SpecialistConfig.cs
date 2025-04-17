using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    public class SpecialistConfig : IEntityTypeConfiguration<Specialist>
    {
        public void Configure(EntityTypeBuilder<Specialist> builder)
        {
            builder.ToTable("Specialists");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Specialty).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(100);
            
            builder.HasMany(s => s.Schedules).WithOne(s => s.Specialist).HasForeignKey(s => s.SpecialistId);
            builder.HasMany(s => s.Appointments).WithOne(a => a.Specialist).HasForeignKey(a => a.SpecialistId);
        }
    }
}
