using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBearServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorChildProfileSubscriptionV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildProfiles_SubscriptionPlans_SubscriptionPlanId",
                table: "ChildProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ChildProfiles_SubscriptionPlanId",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "GraceEndUtc",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndUtc",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanId",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "SubscriptionStartUtc",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "SubscriptionStatus",
                table: "ChildProfiles");

            migrationBuilder.AddColumn<int>(
                name: "subscription_plan_id",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "ChildProfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7925));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7927));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7934));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7935));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7938));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7947));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7948));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7949));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8070), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8076), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8077) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "subscription_plan_id", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8135), new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8103), null, new DateTime(2026, 4, 24, 14, 50, 50, 702, DateTimeKind.Utc).AddTicks(8136) });

            migrationBuilder.CreateIndex(
                name: "IX_users_subscription_plan_id",
                table: "users",
                column: "subscription_plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfiles_user_id",
                table: "ChildProfiles",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildProfiles_users_user_id",
                table: "ChildProfiles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_SubscriptionPlans_subscription_plan_id",
                table: "users",
                column: "subscription_plan_id",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildProfiles_users_user_id",
                table: "ChildProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_users_SubscriptionPlans_subscription_plan_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_subscription_plan_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_ChildProfiles_user_id",
                table: "ChildProfiles");

            migrationBuilder.DropColumn(
                name: "subscription_plan_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "ChildProfiles");

            migrationBuilder.AddColumn<DateTime>(
                name: "GraceEndUtc",
                table: "ChildProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndUtc",
                table: "ChildProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPlanId",
                table: "ChildProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionStartUtc",
                table: "ChildProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionStatus",
                table: "ChildProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5415));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5416));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 4,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 5,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 6,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 10,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 11,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 12,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 13,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 14,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 15,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5434));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 16,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5436));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 20,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5437));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 21,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 22,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 23,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5443));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 24,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5445));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 25,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5447));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 26,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5449));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 27,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5450));

            migrationBuilder.UpdateData(
                table: "banned_words",
                keyColumn: "id",
                keyValue: 28,
                column: "created_at",
                value: new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5634), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5635) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5644), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5645) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "created_at", "free_daily_credits_last_reset", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5772), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5712), new DateTime(2026, 4, 24, 14, 37, 2, 85, DateTimeKind.Utc).AddTicks(5773) });

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfiles_SubscriptionPlanId",
                table: "ChildProfiles",
                column: "SubscriptionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildProfiles_SubscriptionPlans_SubscriptionPlanId",
                table: "ChildProfiles",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
