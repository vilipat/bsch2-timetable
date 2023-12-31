using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timetable.Db.Migrations
{
    /// <inheritdoc />
    public partial class ActivitySlotPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "ActivitySlots",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "ActivitySlots");
        }
    }
}
