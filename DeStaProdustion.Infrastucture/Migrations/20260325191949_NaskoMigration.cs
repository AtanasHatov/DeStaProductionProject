using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeStaProduction.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class NaskoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Performances_PerformanceId",
                table: "Schedules");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerformanceId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Performances_PerformanceId",
                table: "Schedules",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Performances_PerformanceId",
                table: "Schedules");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerformanceId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Performances_PerformanceId",
                table: "Schedules",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
