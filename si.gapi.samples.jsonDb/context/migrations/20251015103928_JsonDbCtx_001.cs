using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace si.gapi.samples.jsonDb.context.migrations
{
    /// <inheritdoc />
    public partial class JsonDbCtx_001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    age = table.Column<int>(type: "INTEGER", nullable: true),
                    createdDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "current_timestamp"),
                    modifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "current_timestamp"),
                    addressListOne = table.Column<string>(type: "TEXT", nullable: true),
                    addressListTwo = table.Column<string>(type: "TEXT", nullable: true),
                    addressPrimary = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_firstName_lastName",
                table: "persons",
                columns: new[] { "firstName", "lastName" });

            migrationBuilder.CreateIndex(
                name: "IX_persons_id",
                table: "persons",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
