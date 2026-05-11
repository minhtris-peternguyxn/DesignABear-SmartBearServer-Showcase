using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePersonalizationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1821));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1824));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1826));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1828));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1829));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1830));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1831));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1832));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1833));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1834));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1835));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1836));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 43, 40, 155, DateTimeKind.Utc).AddTicks(1837));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9096));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9100));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9102));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9103));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9105));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9106));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9107));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9108));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9110));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9111));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9112));
        }
    }
}
