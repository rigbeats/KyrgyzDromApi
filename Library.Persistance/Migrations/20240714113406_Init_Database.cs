using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Init_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Firstname = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Lastname = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(65)", maxLength: 65, nullable: false),
                    PasswordSalt = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserVerificationCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VerificationCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVerificationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVerificationCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationCodes_UserId",
                table: "UserVerificationCodes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVerificationCodes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
