using BLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Database
{
    public class TheraFlowDbContext : DbContext
    {
        public TheraFlowDbContext(DbContextOptions<TheraFlowDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<ConsultationNote> ConsultationNotes => Set<ConsultationNote>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Specialities)
                .WithMany(s => s.Specialists);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Note)
                .WithOne(n => n.Appointment)
                .HasForeignKey<ConsultationNote>(n => n.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(u => u.AppointmentsAsClient)
                .HasForeignKey(a => a.ClientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Specialist)
                .WithMany(u => u.AppointmentsAsSpecialist)
                .HasForeignKey(a => a.SpecialistId);

            modelBuilder.Entity<Speciality>().HasData(
                new Speciality { Id = 1, Name = "Когнітивно-поведінкова терапія" },
                new Speciality { Id = 2, Name = "Гештальт-терапія" },
                new Speciality { Id = 3, Name = "Психоаналітична терапія" }
            );

            //var hasher = new PasswordHasher<User>();

            //var admin = new User
            //{
            //    Id = "admin1",
            //    UserName = "admin@theraflow.com",
            //    NormalizedUserName = "ADMIN@THERAFLOW.COM",
            //    Email = "admin@theraflow.com",
            //    NormalizedEmail = "ADMIN@THERAFLOW.COM",
            //    EmailConfirmed = true,
            //    FullName = "Admin User",
            //    UserType = (int)UserType.Admin,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //};
            //admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            //var client1 = new User
            //{
            //    Id = "client1",
            //    UserName = "ivan@example.com",
            //    NormalizedUserName = "IVAN@EXAMPLE.COM",
            //    Email = "ivan@example.com",
            //    NormalizedEmail = "IVAN@EXAMPLE.COM",
            //    EmailConfirmed = true,
            //    FullName = "Іван Іванов",
            //    DateOfBirth = new DateTime(1990, 5, 12, 0, 0, 0, DateTimeKind.Utc),
            //    UserType = (int)UserType.Client,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //};
            //client1.PasswordHash = hasher.HashPassword(client1, "Client123!");

            //var client2 = new User
            //{
            //    Id = "client2",
            //    UserName = "maria@example.com",
            //    NormalizedUserName = "MARIA@EXAMPLE.COM",
            //    Email = "maria@example.com",
            //    NormalizedEmail = "MARIA@EXAMPLE.COM",
            //    EmailConfirmed = true,
            //    FullName = "Марія Коваль",
            //    DateOfBirth = new DateTime(1995, 8, 22, 0, 0, 0, DateTimeKind.Utc),
            //    UserType = (int)UserType.Client,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //};
            //client2.PasswordHash = hasher.HashPassword(client2, "Client123!");

            //var specialist = new User
            //{
            //    Id = "spec1",
            //    UserName = "olena@theraflow.com",
            //    NormalizedUserName = "OLENA@THERAFLOW.COM",
            //    Email = "olena@theraflow.com",
            //    NormalizedEmail = "OLENA@THERAFLOW.COM",
            //    EmailConfirmed = true,
            //    FullName = "Олена Психолог",
            //    UserType = (int)UserType.Specialist,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //};
            //specialist.PasswordHash = hasher.HashPassword(specialist, "Specialist123!");

            //modelBuilder.Entity<User>().HasData(admin, client1, client2, specialist);
        }
    }
}
