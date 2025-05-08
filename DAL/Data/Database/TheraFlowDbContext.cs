using BLL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Database
{
    public class TheraFlowDbContext : IdentityDbContext<User>
    {
        public TheraFlowDbContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ConsultationNote> ConsultationNotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(w =>
                w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>()
                .ToTable("RefreshTokens")
                .HasOne(x => x.User).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.UserId);

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

            //modelBuilder.Entity<Speciality>().HasData(
            //    new Speciality { Id = 1, Name = "Когнітивно-поведінкова терапія" },
            //    new Speciality { Id = 2, Name = "Гештальт-терапія" },
            //    new Speciality { Id = 3, Name = "Психоаналітична терапія" }
            //);

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = "admin",
            //        UserName = "admin@theraflow.com",
            //        NormalizedUserName = "ADMIN@THERAFLOW.COM",
            //        Email = "admin@theraflow.com",
            //        NormalizedEmail = "ADMIN@THERAFLOW.COM",
            //        EmailConfirmed = true,
            //        FullName = "Admin User",
            //        UserType = (int)UserType.Admin,
            //        PasswordHash = "AQAAAAIAAYagAAAAECbluuqoFGkzU2jkBa9rEknC3B49fwPIk6jGgrxkayiQzy1G51tvODZVdbTB2QurBw==",
            //        SecurityStamp = "UBZYFMAF4E3FBYZONVK4AZTWGNCOZAVH",
            //    },
            //    new User
            //    {
            //        Id = "7ac90941-f53c-4db8-8037-22bfbde07420",
            //        UserName = "client1",
            //        NormalizedUserName = "CLIENT1",
            //        Email = "client1@theraflow.com",
            //        NormalizedEmail = "CLIENT1@THERAFLOW.COM",
            //        EmailConfirmed = true,
            //        FullName = "Андрій Грицюк",
            //        UserType = (int)UserType.Client,
            //        PasswordHash = "AQAAAAIAAYagAAAAEHhOW4zPVgfP7MWbDlvGM3QP9sPap6jFpVhyz+tjWFljAFIgBTih/sD+sQ1A5FF5ng==",
            //        SecurityStamp = "BB4CUDIKFZ54QHRE2DDYK7RSSWTWH5X6",
            //    },
            //    new User
            //    {
            //        Id = "1fc01305-15c9-484e-b8eb-75b1ca83d94f",
            //        UserName = "client2",
            //        NormalizedUserName = "CLIENT2",
            //        Email = "client2@theraflow.com",
            //        NormalizedEmail = "CLIENT2@THERAFLOW.COM",
            //        EmailConfirmed = true,
            //        FullName = "Анна Александрук",
            //        UserType = (int)UserType.Client,
            //        PasswordHash = "AQAAAAIAAYagAAAAEFVMo2lJ6IrfNnP2oIdMARmSq5uiJQsc6BRc/rY71zr7kCmG4+Sbndo3SQkexVxjyQ==",
            //        SecurityStamp = "CR5I7PVTS2EO3GHPWOGISX76X2MYU3QI",
            //    },
            //    new User
            //    {
            //        Id = "2193f1c9-5021-4229-9108-95372e604b93",
            //        UserName = "specialist1",
            //        NormalizedUserName = "SPECIALIST1",
            //        Email = "specialst1@theraflow.com",
            //        NormalizedEmail = "SPECIALST1@THERAFLOW.COM",
            //        EmailConfirmed = true,
            //        FullName = "Єлизавета Опанасець",
            //        UserType = (int)UserType.Specialist,
            //        PasswordHash = "AQAAAAIAAYagAAAAEIx/cnQyQ3gOibzJsLixd+RO+HrnTFkgBLX+C3a5gIOBWwOqIZ6NoxgO6jsOhSULlQ==",
            //        SecurityStamp = "Y7OOUJA5OOD45G5NOYXKBBENF7IGEM3I",
            //    }
            // );
        }
    }
}
