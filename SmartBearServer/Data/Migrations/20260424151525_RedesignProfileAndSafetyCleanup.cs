using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RedesignProfileAndSafetyCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedEndHour",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "AllowedStartHour",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "BearCategory",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "SubscribedSubjects",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "WhitelistTopics",
                table: "ChildProfiles");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3700));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3704));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3716));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3717));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3718));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3719));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3721));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3723));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3724));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3837), new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3838) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3841), new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3842) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3880), new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3859), new DateTime(2026, 4, 24, 15, 15, 24, 631, DateTimeKind.Utc).AddTicks(3880) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedEndHour",
                table: "ChildProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AllowedStartHour",
                table: "ChildProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BearCategory",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubscribedSubjects",
                table: "ChildProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhitelistTopics",
                table: "ChildProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7925));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7927));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7934));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7935));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7938));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7947));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7948));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7949));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8070), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8076), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8077) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8135), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8103), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8136) });
        }
    }
}
