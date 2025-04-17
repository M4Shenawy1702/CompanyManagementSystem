using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangePaymentRelationRo1roM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPayrolls");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payrolls",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_UserId",
                table: "Payrolls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_AspNetUsers_UserId",
                table: "Payrolls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_AspNetUsers_UserId",
                table: "Payrolls");

            migrationBuilder.DropIndex(
                name: "IX_Payrolls_UserId",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payrolls");

            migrationBuilder.CreateTable(
                name: "UserPayrolls",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PayrollId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayrolls", x => new { x.UserId, x.PayrollId });
                    table.ForeignKey(
                        name: "FK_UserPayrolls_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPayrolls_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPayrolls_PayrollId",
                table: "UserPayrolls",
                column: "PayrollId");
        }
    }
}
