using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LdelossantosN5.Domain.Impl.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CQRSSEmployeeSecurityEntitySet",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CQRSSEmployeeSecurityEntitySet", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "SEC_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEC_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SEC_PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEC_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SEC_EmployeePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEC_EmployeePermissions", x => x.Id);
                    table.UniqueConstraint("AK_SEC_EmployeePermissions_EmployeeId_PermissionTypeId", x => new { x.EmployeeId, x.PermissionTypeId });
                    table.ForeignKey(
                        name: "FK_SEC_EmployeePermissions_SEC_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "SEC_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SEC_EmployeePermissions_SEC_PermissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "SEC_PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SEC_Employees",
                columns: new[] { "Id", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, "Leandro", new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, "Mariela", new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, "Alberto", new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, "Isabel", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "SEC_PermissionTypes",
                columns: new[] { "Id", "Description", "ShortName" },
                values: new object[,]
                {
                    { 1, "Enable the creation of new clients", "Create Client" },
                    { 2, "Enable the edition of clients personal data", "Edit Client" },
                    { 3, "Enable to block the client requesting authorization for any financial operation", "Block Client" },
                    { 4, "Signal the client as suspicios of fraudulent activities", "Mark Client as suspect" }
                });

            migrationBuilder.InsertData(
                table: "SEC_EmployeePermissions",
                columns: new[] { "Id", "EmployeeId", "PermissionTypeId", "RequestStatus" },
                values: new object[,]
                {
                    { 1, 1, 1, 0 },
                    { 2, 1, 2, 1 },
                    { 3, 1, 3, 2 },
                    { 4, 2, 2, 0 },
                    { 5, 3, 3, 0 },
                    { 6, 4, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SEC_EmployeePermissions_PermissionTypeId",
                table: "SEC_EmployeePermissions",
                column: "PermissionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CQRSSEmployeeSecurityEntitySet");

            migrationBuilder.DropTable(
                name: "SEC_EmployeePermissions");

            migrationBuilder.DropTable(
                name: "SEC_Employees");

            migrationBuilder.DropTable(
                name: "SEC_PermissionTypes");
        }
    }
}
