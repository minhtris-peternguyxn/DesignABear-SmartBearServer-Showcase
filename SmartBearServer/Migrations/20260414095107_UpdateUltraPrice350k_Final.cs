using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUltraPrice350k_Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 3,
                column: "PriceMonthly",
                value: 350000m);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 9, 51, 7, 103, DateTimeKind.Utc).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 9, 51, 7, 103, DateTimeKind.Utc).AddTicks(6827));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 9, 51, 7, 103, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 9, 51, 7, 103, DateTimeKind.Utc).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 9, 51, 7, 103, DateTimeKind.Utc).AddTicks(6830));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 3,
                column: "PriceMonthly",
                value: 499000m);

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
        }
    }
}
