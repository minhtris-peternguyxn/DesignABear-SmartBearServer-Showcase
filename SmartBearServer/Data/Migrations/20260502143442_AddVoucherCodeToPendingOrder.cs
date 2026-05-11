using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVoucherCodeToPendingOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.AddColumn<string>(
                name: "voucher_code",
                table: "pending_orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

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
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2111), "dam nhau" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2113), "danh nhau" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "word" },
                values: new object[] { new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2114), "tu vong" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2120), "mau me" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "drug", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2124), "ma tuy" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "drug", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2125), "can sa" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "created_at", "word" },
                values: new object[] { new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2129), "thuoc phien" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "drug", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2131), "hit ke" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "alcohol", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2132), "uong ruou" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "tobacco", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2134), "hut thuoc" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2135), "sex" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2136), "phim nguoi lon" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2138), "khieu dam" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2139), "dam o" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2143), "裸" });

            migrationBuilder.InsertData(
                table: "banned_words",
                columns: new[] { "id", "category", "created_at", "created_by", "is_active", "user_id", "word" },
                values: new object[,]
                {
                    { 7, "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2122), "system", true, null, "bat coc" },
                    { 30, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2144), "system", true, null, "vu khi" },
                    { 31, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2361), "system", true, null, "sung luc" },
                    { 32, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2365), "system", true, null, "luu dan" },
                    { 33, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2366), "system", true, null, "bom" },
                    { 34, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2368), "system", true, null, "thuoc no" },
                    { 35, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2369), "system", true, null, "dao gam" },
                    { 50, "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2370), "system", true, null, "suicide" },
                    { 51, "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2372), "system", true, null, "kill someone" },
                    { 52, "drug", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2373), "system", true, null, "drugs" },
                    { 53, "adult", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2375), "system", true, null, "porn" },
                    { 54, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2376), "system", true, null, "weapon" },
                    { 55, "weapons", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2377), "system", true, null, "bomb" },
                    { 56, "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2379), "system", true, null, "terrorist" },
                    { 57, "violence", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2380), "system", true, null, "murder" },
                    { 100, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2382), "system", true, null, "ignore previous instructions" },
                    { 101, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2386), "system", true, null, "ignore all instructions" },
                    { 102, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2387), "system", true, null, "system prompt" },
                    { 103, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2389), "system", true, null, "developer message" },
                    { 104, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2390), "system", true, null, "pretend you are" },
                    { 105, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2392), "system", true, null, "act as if you are" },
                    { 106, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2393), "system", true, null, "jailbreak" },
                    { 107, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2395), "system", true, null, "dan mode" },
                    { 108, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2396), "system", true, null, "do anything now" },
                    { 109, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2397), "system", true, null, "bypass filter" },
                    { 110, "AI_SAFETY", new DateTime(2026, 5, 2, 14, 34, 40, 688, DateTimeKind.Utc).AddTicks(2399), "system", true, null, "you are no longer an ai" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DropColumn(
                name: "voucher_code",
                table: "pending_orders");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8478));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8482));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "drug", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8485), "ma tuy" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8486), "sex" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "word" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8488), "vu khi" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "weapons", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8489), "bom" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "violence", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8491), "suicide" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "violence", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8492), "kill" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "created_at", "word" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8494), "drugs" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "adult", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8496), "porn" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "weapons", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8497), "weapon" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "weapons", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8499), "bomb" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8502), "ignore previous instructions" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8503), "ignore all instructions" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8505), "system prompt" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8506), "developer message" });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "category", "created_at", "word" },
                values: new object[] { "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8508), "pretend you are" });

            migrationBuilder.InsertData(
                table: "banned_words",
                columns: new[] { "id", "category", "created_at", "created_by", "is_active", "user_id", "word" },
                values: new object[,]
                {
                    { 16, "violence", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8500), "system", true, null, "terrorist" },
                    { 25, "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8509), "system", true, null, "act as if you are" },
                    { 26, "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8511), "system", true, null, "jailbreak" },
                    { 27, "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8512), "system", true, null, "dan mode" },
                    { 28, "AI_SAFETY", new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8514), "system", true, null, "do anything now" }
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8635), new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8636) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8677), new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8678) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8810), new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8734), new DateTime(2026, 4, 26, 1, 40, 28, 625, DateTimeKind.Utc).AddTicks(8812) });
        }
    }
}
