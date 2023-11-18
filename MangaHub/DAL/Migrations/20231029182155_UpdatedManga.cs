using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class UpdatedManga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Mangas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleasedOn",
                table: "Mangas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Mangas",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 10, 29, 18, 21, 54, 733, DateTimeKind.Utc).AddTicks(3248));

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_UserId",
                table: "Mangas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Users_UserId",
                table: "Mangas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Users_UserId",
                table: "Mangas");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_UserId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "ReleasedOn",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Mangas");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 10, 29, 17, 25, 41, 525, DateTimeKind.Utc).AddTicks(2523));
        }
    }
}
