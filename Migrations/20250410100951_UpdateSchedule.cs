using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorReservation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "magePath",
                table: "Doctors",
                newName: "ImagePath");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleStatus",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleStatus",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Doctors",
                newName: "magePath");
        }
    }
}
