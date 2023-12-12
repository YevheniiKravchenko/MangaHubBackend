using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Mangaidisnullableincomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Mangas_MangaId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "MangaId",
                table: "Comments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 12, 12, 17, 12, 50, 401, DateTimeKind.Utc).AddTicks(2911));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Mangas_MangaId",
                table: "Comments",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "MangaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Mangas_MangaId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "MangaId",
                table: "Comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 12, 12, 17, 6, 26, 181, DateTimeKind.Utc).AddTicks(6770));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Mangas_MangaId",
                table: "Comments",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "MangaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
