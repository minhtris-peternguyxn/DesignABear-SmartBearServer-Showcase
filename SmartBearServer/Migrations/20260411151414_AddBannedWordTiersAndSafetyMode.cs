using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddBannedWordTiersAndSafetyMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_admin",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SafetyResponseMode",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "banned_words",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6628), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6632), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6633), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6634), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6635), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6636), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6637), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6638), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6639), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6640), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6640), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6641), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6642), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6643), null });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "created_at", "user_id" },
                values: new object[] { new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6644), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_admin",
                table: "users");

            migrationBuilder.DropColumn(
                name: "SafetyResponseMode",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "banned_words");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9388));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9398));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9399));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9400));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9401));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9405));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9405));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 9, 48, 713, DateTimeKind.Utc).AddTicks(9406));
        }
    }
}
