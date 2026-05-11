using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartBearServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banned_words",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    word = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banned_words", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DailyUsages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProfileId = table.Column<string>(type: "text", nullable: false),
                    DateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AiRequestCount = table.Column<int>(type: "integer", nullable: false),
                    AudioSecondsUsed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCredentials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeviceId = table.Column<string>(type: "text", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    AudioUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlanType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CanPlayMusic = table.Column<bool>(type: "boolean", nullable: false),
                    CanTellStoriesOnUserSpeech = table.Column<bool>(type: "boolean", nullable: false),
                    CanUseLearningAI = table.Column<bool>(type: "boolean", nullable: false),
                    PriceMonthly = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    provider = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    provider_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    refresh_token = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "ChildProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "integer", nullable: true),
                    SubscribedSubjects = table.Column<string>(type: "text", nullable: false),
                    SubscriptionStatus = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionStartUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SubscriptionEndUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GraceEndUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AllowedStartHour = table.Column<int>(type: "integer", nullable: true),
                    AllowedEndHour = table.Column<int>(type: "integer", nullable: true),
                    BlockedTopics = table.Column<string>(type: "text", nullable: false),
                    WhitelistTopics = table.Column<string>(type: "text", nullable: false),
                    CurrentMode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildProfiles_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "bear_devices",
                columns: table => new
                {
                    device_id = table.Column<string>(type: "text", nullable: false),
                    serial_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    profile_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bear_devices", x => x.device_id);
                    table.ForeignKey(
                        name: "FK_bear_devices_ChildProfiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "ChildProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_bear_devices_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InteractionHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Request = table.Column<string>(type: "text", nullable: false),
                    Response = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChildProfileId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteractionHistories_ChildProfiles_ChildProfileId",
                        column: x => x.ChildProfileId,
                        principalTable: "ChildProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "smart_alarms",
                columns: table => new
                {
                    alarm_id = table.Column<string>(type: "text", nullable: false),
                    device_id = table.Column<string>(type: "text", nullable: false),
                    hour = table.Column<int>(type: "integer", nullable: false),
                    minute = table.Column<int>(type: "integer", nullable: false),
                    wake_up_message = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_smart_alarms", x => x.alarm_id);
                    table.ForeignKey(
                        name: "FK_smart_alarms_bear_devices_device_id",
                        column: x => x.device_id,
                        principalTable: "bear_devices",
                        principalColumn: "device_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "CanPlayMusic", "CanTellStoriesOnUserSpeech", "CanUseLearningAI", "Description", "IsActive", "Name", "PlanType", "PriceMonthly" },
                values: new object[,]
                {
                    { 1, true, true, false, "Basic music and stories.", true, "Basic Plan", 1, 0m },
                    { 2, true, true, true, "Unlimited AI Learning.", true, "Premium Plan", 2, 99m }
                });

            migrationBuilder.InsertData(
                table: "banned_words",
                columns: new[] { "id", "category", "created_at", "created_by", "is_active", "word" },
                values: new object[,]
                {
                    { 1, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6518), "system", true, "tu sat" },
                    { 2, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6523), "system", true, "giet nguoi" },
                    { 3, "drug", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6525), "system", true, "ma tuy" },
                    { 4, "adult", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6527), "system", true, "sex" },
                    { 5, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6529), "system", true, "vu khi" },
                    { 6, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6613), "system", true, "suicide" },
                    { 7, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6616), "system", true, "kill" },
                    { 8, "drug", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6618), "system", true, "drugs" },
                    { 9, "adult", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6619), "system", true, "porn" },
                    { 10, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6621), "system", true, "weapon" },
                    { 11, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6623), "system", true, "bomb" },
                    { 12, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6624), "system", true, "terrorist" },
                    { 13, "violence", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6626), "system", true, "dao" },
                    { 14, "security", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6628), "system", true, "hack" },
                    { 15, "security", new DateTime(2026, 4, 10, 15, 32, 55, 153, DateTimeKind.Utc).AddTicks(6629), "system", true, "password" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_banned_words_word",
                table: "banned_words",
                column: "word",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bear_devices_profile_id",
                table: "bear_devices",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_bear_devices_user_id",
                table: "bear_devices",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfiles_SubscriptionPlanId",
                table: "ChildProfiles",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyUsages_ProfileId_DateUtc",
                table: "DailyUsages",
                columns: new[] { "ProfileId", "DateUtc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCredentials_DeviceId_TokenHash",
                table: "DeviceCredentials",
                columns: new[] { "DeviceId", "TokenHash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InteractionHistories_ChildProfileId",
                table: "InteractionHistories",
                column: "ChildProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_smart_alarms_device_id",
                table: "smart_alarms",
                column: "device_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banned_words");

            migrationBuilder.DropTable(
                name: "DailyUsages");

            migrationBuilder.DropTable(
                name: "DeviceCredentials");

            migrationBuilder.DropTable(
                name: "InteractionHistories");

            migrationBuilder.DropTable(
                name: "smart_alarms");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "bear_devices");

            migrationBuilder.DropTable(
                name: "ChildProfiles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
