using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddNicknameToDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nickname",
                table: "bear_devices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nickname",
                table: "bear_devices");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6518));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6525));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6527));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6529));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6613));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6618));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6619));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6621));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6623));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6624));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6626));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6628));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6629));
        }
    }
}
