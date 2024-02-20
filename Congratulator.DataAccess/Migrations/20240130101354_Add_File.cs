using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Congratulator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_File : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Birthdays",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Birthdays");
        }
    }
}
