using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedDefaultAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "IsAdmin", "Login", "PasswordHash", "PasswordSalt", "RegistrationDate" },
                values: new object[] { 1, true, "Admin", "$2a$10$WkrWKFdubfRwcY4MjdFELui7Dh8r3ykAvDYOQPvQud0vPlxFHVen.", "d!W2~4~zI{wq:l<p", new DateTime(2023, 10, 23, 20, 49, 1, 628, DateTimeKind.Utc).AddTicks(1009) });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "UserId", "Avatar", "BirthDate", "Description", "Email", "FirstName", "LastName", "PhoneNumber", "ShowConfidentialInformation" },
                values: new object[] { 1, new byte[0], new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Main administrator of the service", "admin@mangahub.com", "Admin", "Admin", "0505050505", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Profiles",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
