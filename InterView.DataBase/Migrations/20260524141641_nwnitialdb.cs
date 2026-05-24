using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterView.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class nwnitialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.CreateTable(
                name: "Dictionaries",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionaries_Dictionaries_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "General",
                        principalTable: "Dictionaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Dictionaries_UserClassId",
                        column: x => x.UserClassId,
                        principalSchema: "General",
                        principalTable: "Dictionaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Dictionaries_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "General",
                        principalTable: "Dictionaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Dictionaries_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "General",
                        principalTable: "Dictionaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lesson_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "General",
                        principalTable: "Lesson",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exam_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "General",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionaries_ParentId",
                schema: "General",
                table: "Dictionaries",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_LessonId",
                schema: "General",
                table: "Exam",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_UserId",
                schema: "General",
                table: "Exam",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_ClassId",
                schema: "General",
                table: "Lesson",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_UserId",
                schema: "General",
                table: "Lesson",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserClassId",
                schema: "General",
                table: "Users",
                column: "UserClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                schema: "General",
                table: "Users",
                column: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exam",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Lesson",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Dictionaries",
                schema: "General");
        }
    }
}
