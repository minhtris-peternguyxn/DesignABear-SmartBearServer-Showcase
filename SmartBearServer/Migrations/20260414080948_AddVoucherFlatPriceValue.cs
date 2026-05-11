using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherFlatPriceValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "flat_price_value",
                table: "vouchers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8913));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8915));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8916));

            migrationBuilder.UpdateData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: 1,
                column: "flat_price_value",
                value: null);

            migrationBuilder.UpdateData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: 2,
                column: "flat_price_value",
                value: 2000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flat_price_value",
                table: "vouchers");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 7, 51, 28, 860, DateTimeKind.Utc).AddTicks(3062));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 7, 51, 28, 860, DateTimeKind.Utc).AddTicks(3069));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 7, 51, 28, 860, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 7, 51, 28, 860, DateTimeKind.Utc).AddTicks(3073));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 7, 51, 28, 860, DateTimeKind.Utc).AddTicks(3074));
        }
    }
}
