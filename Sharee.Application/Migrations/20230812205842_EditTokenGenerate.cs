using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharee.Application.Migrations
{
    /// <inheritdoc />
    public partial class EditTokenGenerate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "token",
                table: "units",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("6cdb398a-9b2f-4ff2-ad6c-ef3021a7d6fc"),
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "token",
                table: "units",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValue: new Guid("6cdb398a-9b2f-4ff2-ad6c-ef3021a7d6fc"));
        }
    }
}
