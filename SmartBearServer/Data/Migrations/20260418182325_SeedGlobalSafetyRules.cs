using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGlobalSafetyRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "banned_words",
                columns: new[] { "id", "category", "created_at", "created_by", "is_active", "user_id", "word" },
                values: new object[,]
                {
                    { 6, "weapons", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(58), "system", true, null, "bom" },
                    { 10, "violence", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(60), "system", true, null, "suicide" },
                    { 11, "violence", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(62), "system", true, null, "kill" },
                    { 12, "drug", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(64), "system", true, null, "drugs" },
                    { 13, "adult", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(66), "system", true, null, "porn" },
                    { 14, "weapons", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(68), "system", true, null, "weapon" },
                    { 15, "weapons", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(69), "system", true, null, "bomb" },
                    { 16, "violence", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(71), "system", true, null, "terrorist" },
                    { 20, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(73), "system", true, null, "ignore previous instructions" },
                    { 21, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(75), "system", true, null, "ignore all instructions" },
                    { 22, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(77), "system", true, null, "system prompt" },
                    { 23, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(79), "system", true, null, "developer message" },
                    { 24, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(81), "system", true, null, "pretend you are" },
                    { 25, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(83), "system", true, null, "act as if you are" },
                    { 26, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(85), "system", true, null, "jailbreak" },
                    { 27, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(87), "system", true, null, "dan mode" },
                    { 28, "AI_SAFETY", new DateTime(2026, 4, 18, 18, 23, 23, 971, DateTimeKind.Utc).AddTicks(88), "system", true, null, "do anything now" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5198));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5392), new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5392) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5400), new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5401) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5536), new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5474), new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5537) });
        }
    }
}
