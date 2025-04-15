using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] VALUES ('{Guid.NewGuid()}', 'Employee', 'EMPLOYEE', '{Guid.NewGuid()}')");
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] VALUES ('{Guid.NewGuid()}', 'Admin', 'ADMIN', '{Guid.NewGuid()}')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM [dbo].[AspNetRoles] WHERE Name = 'Employee'");
            migrationBuilder.Sql($"DELETE FROM [dbo].[AspNetRoles] WHERE Name = 'Admin'");

        }
    }
}
