using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddSafetyMessagesToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SafetyResponseMode",
                table: "ChildProfiles",
                newName: "safety_response_mode");

            migrationBuilder.AddColumn<string>(
                name: "safety_pretend_message",
                table: "ChildProfiles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "safety_warning_message",
                table: "ChildProfiles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1462));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1466));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1467));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1469));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1470));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1471));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1473));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1474));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1475));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1476));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1477));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1478));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "safety_pretend_message",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "safety_warning_message",
                table: "ChildProfiles");

            migrationBuilder.RenameColumn(
                name: "safety_response_mode",
                table: "ChildProfiles",
                newName: "SafetyResponseMode");

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
    }
}
