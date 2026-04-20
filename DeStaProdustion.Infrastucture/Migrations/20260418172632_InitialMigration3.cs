using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeStaProduction.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRequests_AspNetUsers_UserId1",
                table: "TicketRequests");

            migrationBuilder.DropIndex(
                name: "IX_TicketRequests_UserId1",
                table: "TicketRequests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TicketRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TicketRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRequests_UserId",
                table: "TicketRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRequests_AspNetUsers_UserId",
                table: "TicketRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketRequests_AspNetUsers_UserId",
                table: "TicketRequests");

            migrationBuilder.DropIndex(
                name: "IX_TicketRequests_UserId",
                table: "TicketRequests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TicketRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "TicketRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketRequests_UserId1",
                table: "TicketRequests",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRequests_AspNetUsers_UserId1",
                table: "TicketRequests",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
