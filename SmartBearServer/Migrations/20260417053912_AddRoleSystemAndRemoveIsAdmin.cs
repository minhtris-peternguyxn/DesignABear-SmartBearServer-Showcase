using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSystemAndRemoveIsAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.Sql("UPDATE users SET role_id = 1 WHERE is_admin = true;");

            migrationBuilder.DropColumn(
                name: "is_admin",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "refresh_token",
                table: "users",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "provider_id",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "provider",
                table: "users",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_system = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    color_hex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    icon = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Không giới hạn kẹo, giọng đọc cao cấp, học Toán & Tiếng Anh.");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "PriceMonthly" },
                values: new object[] { "Siêu cấp: 300 viên kẹo mỗi ngày, không giới hạn tính năng.", "Gói Ultra", 350000m });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9971));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9975));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 17, 5, 39, 8, 77, DateTimeKind.Utc).AddTicks(9981));

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "role_id", "color_hex", "created_at", "created_by", "description", "icon", "is_active", "is_system", "priority", "role_name", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1, "#FF0000", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(112), null, "System Master Administrator with full access.", "admin_panel_settings", true, true, 0, "Master", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(113), null },
                    { 2, "#00FF00", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(121), null, "Standard regular user.", "person", true, true, 10, "User", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(122), null }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "created_at", "email", "free_daily_credits_last_reset", "full_name", "is_pro", "password_hash", "preferred_tts_provider", "preferred_voice_id", "pro_expires_at", "provider", "provider_id", "refresh_token", "refresh_token_expiry_time", "role_id", "smart_candies", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(192), "master@smartbear.com", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(169), "System Master", false, "$2a$12$MXvDtE/tqBXLEjg7YYobS.7rEzIIuy/LnawiYYkrjNWHDDCBsU4eO", "GCP", "vi-VN-Neural2-A", null, "Local", null, "", null, 1, 10, new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(194) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(203), "user@smartbear.com", new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(197), "Regular User", false, "$2a$12$DcE607AZEF3iweFcrQ3Hsu/jGDs1wbhH0ckN3HyudXTQKt0bJQCRW", "GCP", "vi-VN-Neural2-A", null, "Local", null, "", null, 2, 10, new DateTime(2026, 4, 17, 5, 39, 8, 78, DateTimeKind.Utc).AddTicks(204) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropIndex(
                name: "IX_users_role_id",
                table: "users");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "refresh_token",
                table: "users",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "provider_id",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "provider",
                table: "users",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_admin",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "50 kẹo/ngày, giọng đọc cao cấp, học Toán & Tiếng Anh.");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "PriceMonthly" },
                values: new object[] { "300 kẹo/ngày, không giới hạn tính năng, ưu tiên xử lý.", "Gói VIP (Ultra)", 499000m });

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8913));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8915));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 14, 8, 9, 48, 605, DateTimeKind.Utc).AddTicks(8916));
        }
    }
}
