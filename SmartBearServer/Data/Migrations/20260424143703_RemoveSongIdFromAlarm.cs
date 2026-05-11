using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSongIdFromAlarm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_smart_alarms_Songs_song_id",
                table: "smart_alarms");

            migrationBuilder.DropIndex(
                name: "IX_smart_alarms_song_id",
                table: "smart_alarms");

            migrationBuilder.DropColumn(
                name: "song_id",
                table: "smart_alarms");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5416));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5436));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5437));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5445));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5447));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5449));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5450));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5634), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5635) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5644), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5645) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5772), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5712), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5773) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "song_id",
                table: "smart_alarms",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4137));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4139));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4153));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4154));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4157));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4159));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4163));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4247), new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4248) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4252), new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4252) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4306), new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4275), new DateTime(2026, 4, 24, 14, 20, 28, 894, DateTimeKind.Utc).AddTicks(4307) });

            migrationBuilder.CreateIndex(
                name: "IX_smart_alarms_song_id",
                table: "smart_alarms",
                column: "song_id");

            migrationBuilder.AddForeignKey(
                name: "FK_smart_alarms_Songs_song_id",
                table: "smart_alarms",
                column: "song_id",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
