using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyCandyLimitToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "daily_candy_limit",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6186));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6189));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6192));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6196));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6228));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6229));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6229));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6230));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6232));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6232));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6233));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6236));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6236));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6237));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6237));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6238));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6238));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6239));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6239));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6242));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6242));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6244));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6244));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6245));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6245));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6246));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6246));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6316), new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6317) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6321) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6365), new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6338), new DateTime(2026, 5, 4, 2, 39, 54, 838, DateTimeKind.Utc).AddTicks(6365) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "daily_candy_limit",
                table: "ChildProfiles");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9940));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9959));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9963));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9975));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9977));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9984));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9986));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9987));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9989));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9990));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9992));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9993));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9994));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9998));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 590, DateTimeKind.Utc).AddTicks(9999));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(1));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(14));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(18));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(20));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(22));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(135));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(137));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(139));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(140));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(144));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(145));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(147));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(150));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(151));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(153));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(154));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(156));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(157));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(610), new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(611) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(619), new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(620) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(817), new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(746), new DateTime(2026, 5, 2, 16, 10, 3, 591, DateTimeKind.Utc).AddTicks(818) });
        }
    }
}
