using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Addpayrol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.RenameColumn(
                name: "TotalSalray",
                table: "Payrolls",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "BaseSalray",
                table: "Payrolls",
                newName: "BaseSalary");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Emails",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "Emails",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "ReceiverEmail",
                table: "Emails",
                newName: "From");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Emails",
                newName: "Body");

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "Payrolls",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Payrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripeCheckoutSessionId",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Emails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Emails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "StripeCheckoutSessionId",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Emails");

            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                table: "Payrolls",
                newName: "TotalSalray");

            migrationBuilder.RenameColumn(
                name: "BaseSalary",
                table: "Payrolls",
                newName: "BaseSalray");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Emails",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Emails",
                newName: "SenderEmail");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Emails",
                newName: "ReceiverEmail");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Emails",
                newName: "Message");

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                });
        }
    }
}
