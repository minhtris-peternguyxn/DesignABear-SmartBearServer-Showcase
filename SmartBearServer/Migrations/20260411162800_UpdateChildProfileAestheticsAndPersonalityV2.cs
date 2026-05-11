using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChildProfileAestheticsAndPersonalityV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interaction_histories_ChildProfiles_ChildProfileId",
                table: "interaction_histories");

            migrationBuilder.DropIndex(
                name: "IX_interaction_histories_ChildProfileId",
                table: "interaction_histories");

            migrationBuilder.DropColumn(
                name: "ChildProfileId",
                table: "interaction_histories");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Honorific",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Personality",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalityDescription",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredTtsProvider",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredVoiceId",
                table: "ChildProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "chat_sessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "summary",
                table: "chat_sessions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_sessions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9096));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9100));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9101));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9102));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9103));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 7,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9104));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 8,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9105));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 9,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9106));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9107));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9108));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9110));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9111));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 11, 16, 28, 0, 5, DateTimeKind.Utc).AddTicks(9112));

            migrationBuilder.CreateIndex(
                name: "IX_interaction_histories_profile_id",
                table: "interaction_histories",
                column: "profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_interaction_histories_ChildProfiles_profile_id",
                table: "interaction_histories",
                column: "profile_id",
                principalTable: "ChildProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interaction_histories_ChildProfiles_profile_id",
                table: "interaction_histories");

            migrationBuilder.DropIndex(
                name: "IX_interaction_histories_profile_id",
                table: "interaction_histories");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "Honorific",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "Personality",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "PersonalityDescription",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "PreferredTtsProvider",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "PreferredVoiceId",
                table: "ChildProfiles");

            migrationBuilder.AddColumn<string>(
                name: "ChildProfileId",
                table: "interaction_histories",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "chat_sessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "summary",
                table: "chat_sessions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "chat_sessions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

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
                name: "IX_interaction_histories_ChildProfileId",
                table: "interaction_histories",
                column: "ChildProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_interaction_histories_ChildProfiles_ChildProfileId",
                table: "interaction_histories",
                column: "ChildProfileId",
                principalTable: "ChildProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
