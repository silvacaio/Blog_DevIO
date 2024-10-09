using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_DevIO.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94af7808-1a86-4621-817d-a81f1c40e742", "caiosilva@teste.com", "AQAAAAIAAYagAAAAEMG8sFJRCr2Qsg7FtlC5JfOrN11JYu8tV3JXsRYPyUofY4rqj1SjxucCuXURFG8e3g==", "33570225-9172-4127-805b-a2009353164c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb430f77-8705-454d-b9b3-2a2e3081610d",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78629968-af6e-4f21-ab6a-3c5824052410", "blog@teste.com", "AQAAAAIAAYagAAAAEO1CUN3Cdl5tA+0TzVRFaoZR5POwdkELnBozDV0YcpsKhGM/wzWC0wGVd6xbqxqfMQ==", "b20bb157-f4e3-4c9a-b26e-82518e710a79" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0f6b9dd-4166-4cfb-8d32-00b6409cede3", null, "AQAAAAIAAYagAAAAEGC/7ixLt9rZk9WSiZtSIzC3cx0OK26QvybTepYuPMQRofmQ43N+UhG8Ip9g+cHdUA==", "b6f0554e-a1a5-41e4-b134-e53290a92ce1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb430f77-8705-454d-b9b3-2a2e3081610d",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf140a68-8654-446f-9de3-53d381004a10", null, "AQAAAAIAAYagAAAAEIzTSjcyPfGb17nVbQNmHsxxsI0v+Oy1xIv/mE76a+kO0Es64Rpac0bipdc/XRjYtw==", "4478634f-07d7-4c48-94b3-52395d57a838" });
        }
    }
}
