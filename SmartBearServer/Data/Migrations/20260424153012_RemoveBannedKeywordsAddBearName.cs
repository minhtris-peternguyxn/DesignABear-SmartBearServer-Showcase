using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBannedKeywordsAddBearName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannedKeywords",
                table: "ChildProfiles");

            migrationBuilder.AddColumn<string>(
                name: "BearName",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3547));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3553));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3555));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3556));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3557));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3559));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3560));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3563));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3565));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3567));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3568));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3569));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3571));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3572));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3573));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3574));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3575));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3578));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3624));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3705), new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3706) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3710), new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3761), new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3732), new DateTime(2026, 4, 24, 15, 30, 11, 85, DateTimeKind.Utc).AddTicks(3762) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BearName",
                table: "ChildProfiles");

            migrationBuilder.AddColumn<string>(
                name: "BannedKeywords",
                table: "ChildProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

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
    }
}
