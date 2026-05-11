using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileImageAndPersonalityLab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplexityLevel",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreativityLevel",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmotionLevel",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnergyLevel",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2968));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2973));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2975));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2977));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2979));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2981));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2983));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2985));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2986));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2988));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2993));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2996));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(2999));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3003));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3006));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3139), new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3139) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3144), new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3145) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3213), new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3177), new DateTime(2026, 4, 26, 1, 27, 15, 577, DateTimeKind.Utc).AddTicks(3214) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplexityLevel",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "CreativityLevel",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "EmotionLevel",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "EnergyLevel",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "ChildProfiles");

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
    }
}
