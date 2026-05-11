using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToDemoVoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DemoVoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "DemoVoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8336));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8343));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8348));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8353));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8355));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8359));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8375));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8459), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8459) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8464), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8465) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8524), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8492), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8525) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DemoVoices");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "DemoVoices");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(42));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(50));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(52));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(56));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(58));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(62));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(64));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(66));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(68));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(71));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(73));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(77));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(81));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(85));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(87));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(88));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(281), new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(282) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(290), new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(419), new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(357), new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(420) });
        }
    }
}
