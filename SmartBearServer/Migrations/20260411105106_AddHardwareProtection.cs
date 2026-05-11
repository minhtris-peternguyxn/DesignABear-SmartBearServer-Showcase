using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddHardwareProtection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "free_daily_credits_last_reset",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_pro",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "smart_candies",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "voice_honeys",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "InteractionHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_protected",
                table: "bear_devices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    discount_percentage = table.Column<int>(type: "integer", nullable: true),
                    fixed_discount_value = table.Column<int>(type: "integer", nullable: true),
                    max_usage = table.Column<int>(type: "integer", nullable: true),
                    current_usage = table.Column<int>(type: "integer", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5423));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5425));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5427));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5431));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5433));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 10, 51, 5, 949, DateTimeKind.Utc).AddTicks(5435));

            migrationBuilder.InsertData(
                table: "vouchers",
                columns: new[] { "id", "code", "current_usage", "discount_percentage", "expiry_date", "fixed_discount_value", "is_active", "max_usage" },
                values: new object[] { 1, "DEMO67", 0, null, null, 98000, true, 1000 });

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_code",
                table: "vouchers",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropColumn(
                name: "free_daily_credits_last_reset",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_pro",
                table: "users");

            migrationBuilder.DropColumn(
                name: "smart_candies",
                table: "users");

            migrationBuilder.DropColumn(
                name: "voice_honeys",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "InteractionHistories");

            migrationBuilder.DropColumn(
                name: "is_protected",
                table: "bear_devices");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4622));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4627));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4632));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4633));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4635));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4636));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4638));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4639));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4642));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4643));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 3, 40, 11, 375, DateTimeKind.Utc).AddTicks(4644));
        }
    }
}
