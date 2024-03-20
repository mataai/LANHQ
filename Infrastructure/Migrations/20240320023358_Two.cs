using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetRoles_ApplicationRoleId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AspNetUsers_ApplicationUserId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ApplicationRoleId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ApplicationUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Permissions");

            migrationBuilder.CreateTable(
                name: "ApplicationRolePermission",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RolesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRolePermission", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermission_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermission_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ApplicationUserPermission",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPermission", x => new { x.PermissionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPermission_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPermission_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermission_RolesId",
                table: "ApplicationRolePermission",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPermission_UsersId",
                table: "ApplicationUserPermission",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRolePermission");

            migrationBuilder.DropTable(
                name: "ApplicationUserPermission");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationRoleId",
                table: "Permissions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Permissions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ApplicationRoleId",
                table: "Permissions",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ApplicationUserId",
                table: "Permissions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetRoles_ApplicationRoleId",
                table: "Permissions",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AspNetUsers_ApplicationUserId",
                table: "Permissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
