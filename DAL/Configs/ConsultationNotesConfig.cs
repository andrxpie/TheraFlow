using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    public class ConsultationNotesConfig : IEntityTypeConfiguration<ConsultationNote>
    {
        public void Configure(EntityTypeBuilder<ConsultationNote> builder)
        {
            builder.ToTable("ConsultationNotes");
            builder.HasKey(cn => cn.Id);
            builder.Property(cn => cn.Notes).IsRequired().HasMaxLength(1000);
        }
    }
}
