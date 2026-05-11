using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSongIdToAlarm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8336));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8343));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8348));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8353));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8355));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8359));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8362));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8365));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8375));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8459), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8459) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8464), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8465) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8524), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8492), new DateTime(2026, 4, 23, 14, 46, 54, 632, DateTimeKind.Utc).AddTicks(8525) });
        }
    }
}
