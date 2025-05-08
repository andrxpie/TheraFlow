using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1fc01305-15c9-484e-b8eb-75b1ca83d94f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2193f1c9-5021-4229-9108-95372e604b93");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ac90941-f53c-4db8-8037-22bfbde07420");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "Speciality",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Speciality",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Speciality",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { "1fc01305-15c9-484e-b8eb-75b1ca83d94f", 0, "9de5eab4-8887-42e9-9d21-716d2b384502", null, "client2@theraflow.com", true, "Анна Александрук", false, null, "CLIENT2@THERAFLOW.COM", "CLIENT2", "AQAAAAIAAYagAAAAEFVMo2lJ6IrfNnP2oIdMARmSq5uiJQsc6BRc/rY71zr7kCmG4+Sbndo3SQkexVxjyQ==", null, false, "CR5I7PVTS2EO3GHPWOGISX76X2MYU3QI", false, "client2", 1 },
                    { "2193f1c9-5021-4229-9108-95372e604b93", 0, "ccce5689-d181-496c-8363-832fc353b6f5", null, "specialst1@theraflow.com", true, "Єлизавета Опанасець", false, null, "SPECIALST1@THERAFLOW.COM", "SPECIALIST1", "AQAAAAIAAYagAAAAEIx/cnQyQ3gOibzJsLixd+RO+HrnTFkgBLX+C3a5gIOBWwOqIZ6NoxgO6jsOhSULlQ==", null, false, "Y7OOUJA5OOD45G5NOYXKBBENF7IGEM3I", false, "specialist1", 2 },
                    { "7ac90941-f53c-4db8-8037-22bfbde07420", 0, "647252ac-fdf6-4eb9-bdfe-b0f715be8a8a", null, "client1@theraflow.com", true, "Андрій Грицюк", false, null, "CLIENT1@THERAFLOW.COM", "CLIENT1", "AQAAAAIAAYagAAAAEHhOW4zPVgfP7MWbDlvGM3QP9sPap6jFpVhyz+tjWFljAFIgBTih/sD+sQ1A5FF5ng==", null, false, "BB4CUDIKFZ54QHRE2DDYK7RSSWTWH5X6", false, "client1", 1 },
                    { "admin", 0, "e98574f2-4b0d-4941-b928-478c9939f620", null, "admin@theraflow.com", true, "Admin User", false, null, "ADMIN@THERAFLOW.COM", "ADMIN@THERAFLOW.COM", "AQAAAAIAAYagAAAAECbluuqoFGkzU2jkBa9rEknC3B49fwPIk6jGgrxkayiQzy1G51tvODZVdbTB2QurBw==", null, false, "UBZYFMAF4E3FBYZONVK4AZTWGNCOZAVH", false, "admin@theraflow.com", 0 }
                });

            migrationBuilder.InsertData(
                table: "Speciality",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Когнітивно-поведінкова терапія" },
                    { 2, "Гештальт-терапія" },
                    { 3, "Психоаналітична терапія" }
                });
        }
    }
}
