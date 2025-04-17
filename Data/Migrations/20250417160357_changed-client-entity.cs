using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class changedcliententity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Clients",
                newName: "Location");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StatusId",
                table: "Clients",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Statuses_StatusId",
                table: "Clients",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Statuses_StatusId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_StatusId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Clients",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
