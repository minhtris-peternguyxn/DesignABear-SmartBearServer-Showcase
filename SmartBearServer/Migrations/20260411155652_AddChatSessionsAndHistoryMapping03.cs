using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddChatSessionsAndHistoryMapping03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteractionHistories_ChildProfiles_ChildProfileId",
                table: "InteractionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionHistories",
                table: "InteractionHistories");

            migrationBuilder.RenameTable(
                name: "InteractionHistories",
                newName: "interaction_histories");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "interaction_histories",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "interaction_histories",
                newName: "response");

            migrationBuilder.RenameColumn(
                name: "Request",
                table: "interaction_histories",
                newName: "request");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "interaction_histories",
                newName: "device_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "interaction_histories",
                newName: "history_id");

            migrationBuilder.RenameIndex(
                name: "IX_InteractionHistories_ChildProfileId",
                table: "interaction_histories",
                newName: "IX_interaction_histories_ChildProfileId");

            migrationBuilder.AddColumn<string>(
                name: "preferred_voice_id",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_safe",
                table: "interaction_histories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "profile_id",
                table: "interaction_histories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "safety_violation_category",
                table: "interaction_histories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "session_id",
                table: "interaction_histories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_interaction_histories",
                table: "interaction_histories",
                column: "history_id");

            migrationBuilder.CreateTable(
                name: "chat_sessions",
                columns: table => new
                {
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    profile_id = table.Column<string>(type: "text", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_interaction_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    summary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_sessions", x => x.session_id);
                });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7290));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7296));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7297));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7298));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7299));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7303));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7304));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7305));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 56, 51, 795, DateTimeKind.Utc).AddTicks(7306));

            migrationBuilder.CreateIndex(
                name: "IX_interaction_histories_session_id",
                table: "interaction_histories",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_sessions_profile_id_is_active",
                table: "chat_sessions",
                columns: new[] { "profile_id", "is_active" });

            migrationBuilder.AddForeignKey(
                name: "FK_interaction_histories_ChildProfiles_ChildProfileId",
                table: "interaction_histories",
                column: "ChildProfileId",
                principalTable: "ChildProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_interaction_histories_chat_sessions_session_id",
                table: "interaction_histories",
                column: "session_id",
                principalTable: "chat_sessions",
                principalColumn: "session_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interaction_histories_ChildProfiles_ChildProfileId",
                table: "interaction_histories");

            migrationBuilder.DropForeignKey(
                name: "FK_interaction_histories_chat_sessions_session_id",
                table: "interaction_histories");

            migrationBuilder.DropTable(
                name: "chat_sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_interaction_histories",
                table: "interaction_histories");

            migrationBuilder.DropIndex(
                name: "IX_interaction_histories_session_id",
                table: "interaction_histories");

            migrationBuilder.DropColumn(
                name: "preferred_voice_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_safe",
                table: "interaction_histories");

            migrationBuilder.DropColumn(
                name: "profile_id",
                table: "interaction_histories");

            migrationBuilder.DropColumn(
                name: "safety_violation_category",
                table: "interaction_histories");

            migrationBuilder.DropColumn(
                name: "session_id",
                table: "interaction_histories");

            migrationBuilder.RenameTable(
                name: "interaction_histories",
                newName: "InteractionHistories");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "InteractionHistories",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "response",
                table: "InteractionHistories",
                newName: "Response");

            migrationBuilder.RenameColumn(
                name: "request",
                table: "InteractionHistories",
                newName: "Request");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "InteractionHistories",
                newName: "DeviceId");

            migrationBuilder.RenameColumn(
                name: "history_id",
                table: "InteractionHistories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_interaction_histories_ChildProfileId",
                table: "InteractionHistories",
                newName: "IX_InteractionHistories_ChildProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionHistories",
                table: "InteractionHistories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6628));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6632));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6633));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6635));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6636));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6637));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6638));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6639));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6641));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6642));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6643));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 15, 14, 14, 97, DateTimeKind.Utc).AddTicks(6644));

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionHistories_ChildProfiles_ChildProfileId",
                table: "InteractionHistories",
                column: "ChildProfileId",
                principalTable: "ChildProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
