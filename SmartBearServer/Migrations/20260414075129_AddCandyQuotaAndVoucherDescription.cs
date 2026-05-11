using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddCandyQuotaAndVoucherDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "vouchers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DailyCandyLimit",
                table: "SubscriptionPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "repeat_count",
                table: "smart_alarms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "repeat_mode",
                table: "smart_alarms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "candy_quantity",
                table: "pending_orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "order_type",
                table: "pending_orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "daily_candy_balance",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_quota_reset_utc",
                table: "ChildProfiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "DailyCandyLimit",
                value: 10);

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DailyCandyLimit", "Description" },
                values: new object[] { 50, "50 kẹo/ngày, giọng đọc cao cấp, học Toán & Tiếng Anh." });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "CanPlayMusic", "CanTellStoriesOnUserSpeech", "CanUseLearningAI", "DailyCandyLimit", "Description", "IsActive", "Name", "PlanType", "PriceMonthly" },
                values: new object[] { 3, true, true, true, 300, "300 kẹo/ngày, không giới hạn tính năng, ưu tiên xử lý.", true, "Gói VIP (Ultra)", 3, 499000m });

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

            migrationBuilder.UpdateData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: 1,
                column: "description",
                value: null);

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "code", "current_usage", "description", "discount_percentage", "expiry_date", "fixed_discount_value", "is_active", "max_usage" },
                values: new object[] { 2, "DEMODONGGIA2K", 0, "Mã kiểm thử: Đồng giá tất cả dịch vụ 2,000đ", null, new DateTime(2035, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, 9999 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "vouchers",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "description",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "DailyCandyLimit",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "repeat_count",
                table: "smart_alarms");

            migrationBuilder.DropColumn(
                name: "repeat_mode",
                table: "smart_alarms");

            migrationBuilder.DropColumn(
                name: "candy_quantity",
                table: "pending_orders");

            migrationBuilder.DropColumn(
                name: "order_type",
                table: "pending_orders");

            migrationBuilder.DropColumn(
                name: "daily_candy_balance",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "last_quota_reset_utc",
                table: "ChildProfiles");

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
                value: new DateTime(2026, 4, 14, 3, 59, 36, 293, DateTimeKind.Utc).AddTicks(8574));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 3, 59, 36, 293, DateTimeKind.Utc).AddTicks(8577));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 3, 59, 36, 293, DateTimeKind.Utc).AddTicks(8579));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 3, 59, 36, 293, DateTimeKind.Utc).AddTicks(8580));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 3, 59, 36, 293, DateTimeKind.Utc).AddTicks(8581));
        }
    }
}
