using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_DevIO.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "048a44ed-981e-4cdf-b79f-d4b473158362", "048a44ed-981e-4cdf-b79f-d4b473158362", "BlogUser", "BLOGUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0f6b9dd-4166-4cfb-8d32-00b6409cede3", "AQAAAAIAAYagAAAAEGC/7ixLt9rZk9WSiZtSIzC3cx0OK26QvybTepYuPMQRofmQ43N+UhG8Ip9g+cHdUA==", "b6f0554e-a1a5-41e4-b134-e53290a92ce1" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FistName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "eb430f77-8705-454d-b9b3-2a2e3081610d", 0, "bf140a68-8654-446f-9de3-53d381004a10", "User", "blog@teste.com", true, "User", "Teste", false, null, null, "blog@teste.com", "AQAAAAIAAYagAAAAEIzTSjcyPfGb17nVbQNmHsxxsI0v+Oy1xIv/mE76a+kO0Es64Rpac0bipdc/XRjYtw==", null, false, "4478634f-07d7-4c48-94b3-52395d57a838", false, "blog@teste.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "048a44ed-981e-4cdf-b79f-d4b473158362", "eb430f77-8705-454d-b9b3-2a2e3081610d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "048a44ed-981e-4cdf-b79f-d4b473158362", "eb430f77-8705-454d-b9b3-2a2e3081610d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "048a44ed-981e-4cdf-b79f-d4b473158362");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb430f77-8705-454d-b9b3-2a2e3081610d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c321c1c3-2a8e-467f-8e9a-437d24a511cb", "AQAAAAIAAYagAAAAEAs7wiBNfCLE+sX+2eiOFKpl8dMOrI36rYXV8FRi03AxfoeWzl7FsuDIDpO42YdRzw==", "84894582-33ee-41eb-b7dd-0efa284be280" });
        }
    }
}
