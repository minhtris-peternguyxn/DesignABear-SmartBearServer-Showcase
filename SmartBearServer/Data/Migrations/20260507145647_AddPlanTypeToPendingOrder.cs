using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanTypeToPendingOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "plan_type",
                table: "pending_orders",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-adam",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8386));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-antoni",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8413));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-bella",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-charlie",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-liam",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8391));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-rachel",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8408));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-sarah",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-a",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8344));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-d",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-a",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-b",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-c",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8375));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-d",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8380));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8093));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8096));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8097));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8102));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8234), new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8235) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8238), new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8238) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8275), new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8276), new DateTime(2026, 5, 7, 14, 56, 47, 603, DateTimeKind.Utc).AddTicks(8275) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "plan_type",
                table: "pending_orders");

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-adam",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3066));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-antoni",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3093));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-bella",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3077));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-charlie",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3098));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-liam",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-rachel",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3087));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-sarah",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3082));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-a",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3027));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-d",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3034));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-a",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3040));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-b",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3046));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-c",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3051));

            migrationBuilder.UpdateData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-d",
                column: "CreatedAt",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3056));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2827));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2827));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2837));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2925), new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2925) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2929), new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2929) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2968), new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2969), new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(2968) });
        }
    }
}
