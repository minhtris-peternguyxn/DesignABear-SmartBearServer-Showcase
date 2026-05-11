using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteAndVoucherCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Stories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Songs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ChildProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "bear_devices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2449));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2451));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2461));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2462));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2464));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2465));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2469));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2471));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2473));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2474));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2476));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2478));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2479));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2481));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2485));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2486));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2488));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2490));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2491));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2492));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2494));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2495));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2497));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2498));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2500));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2501));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2504));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2506));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2507));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2511));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2513));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2514));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2516));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2517));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2519));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2520));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2804), new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2805) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2812), new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2813) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2943), new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 2, 15, 29, 6, 452, DateTimeKind.Utc).AddTicks(2945) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "bear_devices");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2110));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2111));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2120));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2122));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2124));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2125));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2129));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2131));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2132));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2134));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2136));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2138));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2139));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2143));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2144));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2366));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2368));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2369));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2373));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2375));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2376));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2377));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2382));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2390));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2392));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2393));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2396));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2397));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2899), new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2907), new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2908) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(3030), new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2985), new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(3031) });
        }
    }
}
