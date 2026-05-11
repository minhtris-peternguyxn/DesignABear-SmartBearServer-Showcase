using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

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
                columns: new[] { "created_at", "email", "free_daily_credits_last_reset", "full_name", "password_hash", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1679), "admin@admin.com", new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1642), "Master Administrator", "$2b$10$wT0E82c2AIdgUf9/C.B2e.mYvG3Z/S0/3H/O/6q7v2j/5k5x7k.7y", new DateTime(2026, 4, 17, 11, 21, 24, 703, DateTimeKind.Utc).AddTicks(1679) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9971));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9975));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9981));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(112), new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(113) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(121), new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(122) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "email", "free_daily_credits_last_reset", "full_name", "password_hash", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(192), "master@smartbear.com", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(169), "System Master", "$2a$12$MXvDtE/tqBXLEjg7YYobS.7rEzIIuy/LnawiYYkrjNWHDDCBsU4eO", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(194) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "free_daily_credits_last_reset", "full_name", "is_pro", "password_hash", "preferred_tts_provider", "preferred_voice_id", "pro_expires_at", "provider", "provider_id", "refresh_token", "refresh_token_expiry_time", "role_id", "smart_candies", "updated_at" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(203), "user@smartbear.com", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(197), "Regular User", false, "$2a$12$DcE607AZEF3iweFcrQ3Hsu/jGDs1wbhH0ckN3HyudXTQKt0bJQCRW", "GCP", "vi-VN-Neural2-A", null, "Local", null, "", null, 2, 10, new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(204) });
        }
    }
}
