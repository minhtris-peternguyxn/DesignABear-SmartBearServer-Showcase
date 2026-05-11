using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullProfileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "profile_id",
                table: "bear_devices",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5918));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5921));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5922));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5923));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5924));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5925));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5926));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5927));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5928));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5930));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5932));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 11, 28, 5, 795, DateTimeKind.Utc).AddTicks(5933));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "profile_id",
                table: "bear_devices",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
        }
    }
}
