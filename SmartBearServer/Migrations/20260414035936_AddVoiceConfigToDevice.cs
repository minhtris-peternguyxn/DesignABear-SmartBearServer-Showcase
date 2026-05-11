using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddVoiceConfigToDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "preferred_speed",
                table: "bear_devices",
                type: "real",
                nullable: false,
                defaultValue: 1f);

            migrationBuilder.AddColumn<float>(
                name: "preferred_volume",
                table: "bear_devices",
                type: "real",
                nullable: false,
                defaultValue: 1.5f);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "preferred_speed",
                table: "bear_devices");

            migrationBuilder.DropColumn(
                name: "preferred_volume",
                table: "bear_devices");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 13, 15, 49, 11, 407, DateTimeKind.Utc).AddTicks(424));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 13, 15, 49, 11, 407, DateTimeKind.Utc).AddTicks(427));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 13, 15, 49, 11, 407, DateTimeKind.Utc).AddTicks(428));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 13, 15, 49, 11, 407, DateTimeKind.Utc).AddTicks(429));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 13, 15, 49, 11, 407, DateTimeKind.Utc).AddTicks(430));
        }
    }
}
