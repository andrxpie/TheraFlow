using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    public class AppointmentsConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AppointmentDate).IsRequired();
            
            builder.HasOne(a => a.Note).WithOne(n => n.Appointment).HasForeignKey<ConsultationNote>(cn => cn.AppointmentId);
        }
    }
}
