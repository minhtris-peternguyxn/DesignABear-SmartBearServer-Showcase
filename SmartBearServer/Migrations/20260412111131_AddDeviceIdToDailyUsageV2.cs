using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceIdToDailyUsageV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vouchers_code",
                table: "vouchers");

            migrationBuilder.DropIndex(
                name: "IX_pending_orders_user_id",
                table: "pending_orders");

            migrationBuilder.DropIndex(
                name: "IX_DailyUsages_ProfileId_DateUtc",
                table: "DailyUsages");

            migrationBuilder.DropIndex(
                name: "IX_banned_words_word",
                table: "banned_words");

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9);

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
                table: "vouchers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "voice_honeys",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "DailyUsages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Trải nghiệm cơ bản: 10 viên kẹo mỗi ngày, nhạc miễn phí.");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Không giới hạn kẹo, giọng đọc cao cấp, học Toán & Tiếng Anh.");

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

            migrationBuilder.CreateIndex(
                name: "IX_DailyUsages_DeviceId_DateUtc",
                table: "DailyUsages",
                columns: new[] { "DeviceId", "DateUtc" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DailyUsages_DeviceId_DateUtc",
                table: "DailyUsages");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DailyUsages");

            migrationBuilder.AddColumn<int>(
                name: "voice_honeys",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Trải nghiệm cơ bản: 50 kẹo + 20 mật ong mỗi ngày, nhạc miễn phí.");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Không giới hạn credits, giọng đọc cao cấp, học Toán & Tiếng Anh.");

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

            migrationBuilder.InsertData(
                table: "banned_words",
                columns: new[] { "id", "category", "created_at", "created_by", "is_active", "user_id", "word" },
                values: new object[,]
                {
                    { 6, "violence", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1470), "system", true, null, "suicide" },
                    { 7, "violence", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1471), "system", true, null, "kill" },
                    { 8, "drug", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1472), "system", true, null, "drugs" },
                    { 9, "adult", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1473), "system", true, null, "porn" },
                    { 10, "violence", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1474), "system", true, null, "weapon" },
                    { 11, "violence", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1474), "system", true, null, "bomb" },
                    { 12, "violence", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1475), "system", true, null, "terrorist" },
                    { 13, "violence", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1476), "system", true, null, "dao" },
                    { 14, "security", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1477), "system", true, null, "hack" },
                    { 15, "security", new DateTime(2026, 4, 12, 5, 22, 18, 851, DateTimeKind.Utc).AddTicks(1478), "system", true, null, "password" }
                });

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "code", "current_usage", "discount_percentage", "expiry_date", "fixed_discount_value", "is_active", "max_usage" },
                values: new object[] { 1, "DEMO67", 0, null, null, 98000, true, 1000 });

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_code",
                table: "vouchers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pending_orders_user_id",
                table: "pending_orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_DailyUsages_ProfileId_DateUtc",
                table: "DailyUsages",
                columns: new[] { "ProfileId", "DateUtc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_banned_words_word",
                table: "banned_words",
                column: "word",
                unique: true);
        }
    }
}
