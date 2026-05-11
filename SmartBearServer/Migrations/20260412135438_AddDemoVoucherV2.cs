using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDemoVoucherV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 13, 54, 37, 960, DateTimeKind.Utc).AddTicks(8756));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 13, 54, 37, 960, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 13, 54, 37, 960, DateTimeKind.Utc).AddTicks(8762));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 13, 54, 37, 960, DateTimeKind.Utc).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 13, 54, 37, 960, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "code", "current_usage", "discount_percentage", "expiry_date", "fixed_discount_value", "is_active", "max_usage" },
                values: new object[] { 1, "DEMO67", 0, null, new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 98000, true, 1000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 11, 11, 31, 220, DateTimeKind.Utc).AddTicks(6390));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 11, 11, 31, 220, DateTimeKind.Utc).AddTicks(6393));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 11, 11, 31, 220, DateTimeKind.Utc).AddTicks(6394));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 11, 11, 31, 220, DateTimeKind.Utc).AddTicks(6395));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 11, 11, 31, 220, DateTimeKind.Utc).AddTicks(6396));
        }
    }
}
