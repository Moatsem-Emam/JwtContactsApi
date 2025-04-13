using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = "0E22B807-5A44-4E8D-9DB6-1C10A1EF6C6A";
            var userRoleId = "2A85C9E3-2C89-4DB1-9B60-2489CE6C23DF";
            var adminUserId = "11111111-1111-1111-1111-111111111111";

            // Insert Roles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { userRoleId, "User", "USER", Guid.NewGuid().ToString() });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { adminRoleId, "Admin", "ADMIN", Guid.NewGuid().ToString() });

            // Insert Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            var passwordHash = hasher.HashPassword(null, "Admin@123");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "FirstName", "LastName", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] {
        adminUserId,
        "Moatsem",
        "Hussain",
        "m0tsem",
        "M0TSEM",
        "motsememamhussain@gmail.com",
        "MOTSEMEMAMHUSSAIN@GMAIL.COM",
        true,
        passwordHash,
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString(),
        true,
        true,
        true,
        0
                    });

            // Assign Admin Role to User
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { adminUserId, adminRoleId });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete UserRoles
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "0E22B807-5A44-4E8D-9DB6-1C10A1EF6C6A" });

            // Delete User
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            // Delete Roles
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0E22B807-5A44-4E8D-9DB6-1C10A1EF6C6A");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2A85C9E3-2C89-4DB1-9B60-2489CE6C23DF");
        }
    }
}
