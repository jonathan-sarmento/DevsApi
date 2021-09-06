using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevsApi.Migrations
{
    public partial class addTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "Developers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createdById",
                table: "Developers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated",
                table: "Developers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updatedById",
                table: "Developers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Developers_createdById",
                table: "Developers",
                column: "createdById");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_updatedById",
                table: "Developers",
                column: "updatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_AspNetUsers_createdById",
                table: "Developers",
                column: "createdById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_AspNetUsers_updatedById",
                table: "Developers",
                column: "updatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_AspNetUsers_createdById",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Developers_AspNetUsers_updatedById",
                table: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_createdById",
                table: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_updatedById",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "created",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "createdById",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "updated",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "updatedById",
                table: "Developers");
        }
    }
}
