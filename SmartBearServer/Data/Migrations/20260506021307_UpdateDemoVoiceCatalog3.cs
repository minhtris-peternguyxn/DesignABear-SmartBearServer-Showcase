using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDemoVoiceCatalog3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DemoVoices",
                columns: new[] { "Id", "CreatedAt", "Description", "IsPremium", "Name", "Provider", "VoiceId" },
                values: new object[,]
                {
                    { "voice-eleven-adam", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3066), "Giọng nam trầm ấm, chuyên nghiệp.", true, "Gấu Adam (Nam)", "ElevenLabs", "pNInz6obpgnuPs397vXP" },
                    { "voice-eleven-antoni", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3093), "Giọng nam chững chạc, đáng tin cậy.", true, "Gấu Antoni (Nam)", "ElevenLabs", "ErXw7ePBqOfDr909BvG6" },
                    { "voice-eleven-bella", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3077), "Giọng nữ nhẹ nhàng, ấm áp.", true, "Gấu Bella (Nữ)", "ElevenLabs", "EXAVITQu4vr4xnSDxMaL" },
                    { "voice-eleven-charlie", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3098), "Giọng nam thân thiện, tự nhiên.", true, "Gấu Charlie (Nam)", "ElevenLabs", "IKne3meq5pC9XdtgXx6M" },
                    { "voice-eleven-liam", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3071), "Giọng nam trẻ trung, đầy năng lượng.", true, "Gấu Liam (Nam)", "ElevenLabs", "TX3LPaxmHKxFfW646Sse" },
                    { "voice-eleven-rachel", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3087), "Giọng nữ rõ ràng, rành mạch.", true, "Gấu Rachel (Nữ)", "ElevenLabs", "MF3mGyEYCl7XYW7L696t" },
                    { "voice-eleven-sarah", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3082), "Giọng nữ truyền cảm, dễ thương.", true, "Gấu Sarah (Nữ)", "ElevenLabs", "Lcf7eeY9feD1p95OmDAn" },
                    { "voice-gcp-a", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3027), "Giọng nữ chuẩn, vui vẻ.", false, "Gấu Chị A (Nữ)", "GCP", "vi-VN-Neural2-A" },
                    { "voice-gcp-d", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3034), "Giọng nam ấm áp, rõ ràng.", false, "Gấu Anh D (Nam)", "GCP", "vi-VN-Neural2-D" },
                    { "voice-gcp-wavenet-a", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3040), "Giọng nữ ngọt ngào, truyền cảm.", false, "Gấu Mẹ A (Nữ)", "GCP", "vi-VN-Wavenet-A" },
                    { "voice-gcp-wavenet-b", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3046), "Giọng nam trầm, vững chãi.", false, "Gấu Bố B (Nam)", "GCP", "vi-VN-Wavenet-B" },
                    { "voice-gcp-wavenet-c", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3051), "Giọng nữ trầm, chững chạc.", false, "Gấu Cô C (Nữ)", "GCP", "vi-VN-Wavenet-C" },
                    { "voice-gcp-wavenet-d", new DateTime(2026, 5, 6, 2, 13, 7, 28, DateTimeKind.Utc).AddTicks(3056), "Giọng nam truyền cảm, dễ nghe.", false, "Gấu Chú D (Nam)", "GCP", "vi-VN-Wavenet-D" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-adam");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-antoni");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-bella");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-charlie");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-liam");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-rachel");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-eleven-sarah");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-a");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-d");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-a");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-b");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-c");

            migrationBuilder.DeleteData(
                table: "DemoVoices",
                keyColumn: "Id",
                keyValue: "voice-gcp-wavenet-d");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7967));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7969));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7972));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7972));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7974));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7975));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7975));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7976));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 30,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 31,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 32,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 33,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7981));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 34,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 35,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 50,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7983));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 51,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7983));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 52,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 53,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 54,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 55,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7985));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 56,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7985));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 57,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 100,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 101,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7987));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 102,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 103,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 104,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 105,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 106,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7989));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 107,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 108,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 109,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7991));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 110,
                column: "created_at",
                value: new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(7991));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8074), new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8074) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8078), new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8079) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8111), new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8094), new DateTime(2026, 5, 6, 2, 10, 46, 146, DateTimeKind.Utc).AddTicks(8111) });
        }
    }
}
