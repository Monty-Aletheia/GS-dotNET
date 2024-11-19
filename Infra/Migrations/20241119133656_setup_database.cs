using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class setup_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    category = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    model = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    power_rating = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    FirebaseId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    UserId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DeviceId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    estimated_usage_hours = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    consumption = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_devices_tb_devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "tb_devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_devices_tb_users_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_users_Email",
                table: "tb_users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_users_FirebaseId",
                table: "tb_users",
                column: "FirebaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_devices_DeviceId",
                table: "user_devices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_user_devices_UserId",
                table: "user_devices",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_devices");

            migrationBuilder.DropTable(
                name: "tb_devices");

            migrationBuilder.DropTable(
                name: "tb_users");
        }
    }
}
