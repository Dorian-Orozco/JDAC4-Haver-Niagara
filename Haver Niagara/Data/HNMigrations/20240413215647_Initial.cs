using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haver_Niagara.Data.HNMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SAPNumber",
                table: "Parts",
                newName: "SAPNumberID");

            migrationBuilder.CreateTable(
                name: "SAPNumbers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPNumbers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SAPNumberID",
                table: "Parts",
                column: "SAPNumberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_SAPNumbers_SAPNumberID",
                table: "Parts",
                column: "SAPNumberID",
                principalTable: "SAPNumbers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_SAPNumbers_SAPNumberID",
                table: "Parts");

            migrationBuilder.DropTable(
                name: "SAPNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Parts_SAPNumberID",
                table: "Parts");

            migrationBuilder.RenameColumn(
                name: "SAPNumberID",
                table: "Parts",
                newName: "SAPNumber");
        }
    }
}
