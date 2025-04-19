using BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Database
{
    public class TheraFlowDbContext : DbContext
    {
        public TheraFlowDbContext(DbContextOptions<TheraFlowDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Specialist> Specialists => Set<Specialist>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<ConsultationNote> ConsultationNotes => Set<ConsultationNote>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Note)
                .WithOne(n => n.Appointment)
                .HasForeignKey<ConsultationNote>(n => n.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Specialist)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SpecialistId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Specialist)
                .WithMany(sp => sp.Schedules)
                .HasForeignKey(s => s.SpecialistId);

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, FullName = "Іван Іванов", Email = "ivan@example.com", Phone = "1234567890", DateOfBirth = new DateTime(1990, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                new Client { Id = 2, FullName = "Марія Коваль", Email = "maria@example.com", Phone = "0987654321", DateOfBirth = new DateTime(1995, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                new Client { Id = 3, FullName = "Оксана Петренко", Email = "oksana@example.com", Phone = "0970011223", DateOfBirth = new DateTime(1988, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                new Client { Id = 4, FullName = "Дмитро Савчук", Email = "dmytro@example.com", Phone = "0934567890", DateOfBirth = new DateTime(1992, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Specialist>().HasData(
                new Specialist { Id = 1, FullName = "Олена Психолог", Email = "olena@theraflow.com", Specialty = "Когнітивно-поведінкова терапія" },
                new Specialist { Id = 2, FullName = "Андрій Терапевт", Email = "andrii@theraflow.com", Specialty = "Гештальт-терапія" },
                new Specialist { Id = 3, FullName = "Світлана Консультант", Email = "svitlana@theraflow.com", Specialty = "Психоаналітична терапія" }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, SpecialistId = 1, StartTime = new DateTime(2025, 4, 10, 10, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2025, 4, 10, 12, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 2, SpecialistId = 2, StartTime = new DateTime(2025, 4, 11, 14, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2025, 4, 11, 16, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 3, SpecialistId = 3, StartTime = new DateTime(2025, 4, 12, 9, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2025, 4, 12, 11, 0, 0, DateTimeKind.Utc) },
                new Schedule { Id = 4, SpecialistId = 1, StartTime = new DateTime(2025, 4, 13, 13, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2025, 4, 13, 15, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, ClientId = 1, SpecialistId = 1, AppointmentDate = new DateTime(2025, 4, 10, 10, 30, 0, DateTimeKind.Utc) },
                new Appointment { Id = 2, ClientId = 2, SpecialistId = 2, AppointmentDate = new DateTime(2025, 4, 11, 14, 30, 0, DateTimeKind.Utc) },
                new Appointment { Id = 3, ClientId = 3, SpecialistId = 3, AppointmentDate = new DateTime(2025, 4, 12, 9, 30, 0, DateTimeKind.Utc) },
                new Appointment { Id = 4, ClientId = 4, SpecialistId = 1, AppointmentDate = new DateTime(2025, 4, 13, 13, 45, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<ConsultationNote>().HasData(
                new ConsultationNote { Id = 1, AppointmentId = 1, Notes = "Обговорили тривожність та техніки заземлення." },
                new ConsultationNote { Id = 2, AppointmentId = 2, Notes = "Перший сеанс. Уточнено цілі терапії." },
                new ConsultationNote { Id = 3, AppointmentId = 3, Notes = "Проведено глибоке опитування минулих травматичних подій." },
                new ConsultationNote { Id = 4, AppointmentId = 4, Notes = "Обговорено тривалість та очікування від терапії." }
            );
        }
    }
}
