using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminHashFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "created_at", "free_daily_credits_last_reset", "password_hash", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5536), new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5474), "$2a$11$wqjr5Joq.8ZCu0WFzKTWS..sobu6cqhPGXPNx4hdHUrkbfFxuuM9K", new DateTime(2026, 4, 17, 11, 33, 43, 493, DateTimeKind.Utc).AddTicks(5537) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1365));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1371));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1373));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1374));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1376));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1553), new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1554) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1560), new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "password_hash", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1679), new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1642), "$2b$10$wT0E82c2AIdgUf9/C.B2e.mYvG3Z/S0/3H/O/6q7v2j/5k5x7k.7y", new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1679) });
        }
    }
}
