using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserPlanBastard.Migrations
{
    public partial class InitialFixVolumeFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VolumeFile",
                table: "Files",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VolumeFile",
                table: "Files",
                type: "nvarchar",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
