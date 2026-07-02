using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dars_5ososy_API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Governorates_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_Favorites_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => new { x.TeacherId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GovernorateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailabilitySlots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    EducationStageId = table.Column<int>(type: "int", nullable: false),
                    EducationSystemId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MaxStudents = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilitySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailabilitySlots_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailabilitySlots_EducationStages_EducationStageId",
                        column: x => x.EducationStageId,
                        principalTable: "EducationStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailabilitySlots_EducationSystems_EducationSystemId",
                        column: x => x.EducationSystemId,
                        principalTable: "EducationSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailabilitySlots_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    AvailabilitySlotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => new { x.StudentId, x.TeacherId, x.AvailabilitySlotId });
                    table.ForeignKey(
                        name: "FK_Bookings_AvailabilitySlots_AvailabilitySlotId",
                        column: x => x.AvailabilitySlotId,
                        principalTable: "AvailabilitySlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EducationStages",
                columns: new[] { "Id", "ArabicName", "EnglishName" },
                values: new object[,]
                {
                    { 1, "المرحلة الابتدائية", "Primary Stage" },
                    { 2, "المرحلة الإعدادية", "Preparatory Stage" },
                    { 3, "المرحلة الثانوية", "Secondary Stage" },
                    { 4, "المرحلة الجامعية", "University Stage" }
                });

            migrationBuilder.InsertData(
                table: "EducationSystems",
                columns: new[] { "Id", "ArabicName", "EnglishName" },
                values: new object[,]
                {
                    { 1, "النظام القديم", "Old System" },
                    { 2, "النظام الحديث", "Modern System" },
                    { 3, "نظام البكالوريا", "Baccalaureate System" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "ArabicName", "EnglishName" },
                values: new object[,]
                {
                    { 1L, "القاهرة الكبرى", "Great Cairo" },
                    { 2L, "الاسكندرية", "Alexandria" },
                    { 3L, "الدلتا", "Al-Delta" },
                    { 4L, "القناة", "Al-Canal" },
                    { 5L, "شمال الصعيد", "North of Al-Saieed" },
                    { 6L, "وسط الصعيد", "Middle of Al-Saieed" },
                    { 7L, "جنوب الصعيد", "South of Al-Saieed" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, null, "Admin", "ADMIN" },
                    { 2L, null, "Teacher", "TEACHER" },
                    { 3L, null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "ARA", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2145), "منهج اللغة العربية واكتساب المهارات اللغوية والنحوية", "اللغة العربية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2150) },
                    { 2L, "ENG", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2154), "منهج اللغة الإنجليزية للاتصال والمحادثة والقواعد", "اللغة الإنجليزية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2154) },
                    { 3L, "SOC", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2156), "منهج الجغرافيا والتاريخ ومبادئ المواطنة", "الدراسات الاجتماعية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2156) },
                    { 4L, "FRN", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2157), "منهج اللغة الأجنبية الثانية - الفرنسية", "اللغة الفرنسية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2157) },
                    { 5L, "GER", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2158), "منهج اللغة الأجنبية الثانية - الألمانية", "اللغة الألمانية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2159) },
                    { 6L, "ITA", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2160), "منهج اللغة الأجنبية الثانية - الإيطالية", "اللغة الإيطالية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2160) },
                    { 7L, "REL", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2161), "منهج التربية الدينية الإسلامية والمسيحية", "التربية الدينية", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2162) },
                    { 8L, "MAT-AR", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2163), "منهج الرياضيات والحساب باللغة العربية", "الرياضيات", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2163) },
                    { 9L, "SCI-AR", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2164), "منهج العلوم العامة باللغة العربية", "العلوم", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2164) },
                    { 10L, "PHY-AR", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2165), "منهج الفيزياء المتقدمة باللغة العربية", "الفيزياء", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2166) },
                    { 11L, "CHE-AR", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2167), "منهج الكيمياء والتفاعلات باللغة العربية", "الكيمياء", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2167) },
                    { 12L, "BIO-AR", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2168), "منهج علم الأحياء والبيئة باللغة العربية", "الأحياء", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2169) },
                    { 13L, "GEO-AR", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2170), "منهج الجيولوجيا وعلوم الأرض باللغة العربية", "الجيولوجيا", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2170) },
                    { 14L, "MAT-EN", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2171), "Mathematics and algebra syllabus in English", "Math", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2172) },
                    { 15L, "SCI-EN", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2173), "General Science syllabus in English", "Science", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2177) },
                    { 16L, "PHY-EN", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2178), "Advanced Physics concepts and mechanics in English", "Physics", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2178) },
                    { 17L, "CHE-EN", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2179), "Organic and inorganic Chemistry syllabus in English", "Chemistry", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2180) },
                    { 18L, "BIO-EN", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2181), "Living organisms and Biology syllabus in English", "Biology", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2181) },
                    { 19L, "HIS", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2182), "منهج التاريخ والحضارات للمرحلة الثانوية", "التاريخ", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2183) },
                    { 20L, "GEO", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2184), "منهج الجغرافيا السياسية والطبيعية للمرحلة الثانوية", "الجغرافيا", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2184) },
                    { 21L, "PHI", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2185), "مبادئ التفكير الفلسفي والمنطق الصوري", "الفلسفة والمنطق", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2185) },
                    { 22L, "PSY", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2187), "دراسة السلوك الإنساني ومبادئ علم الاجتماع", "علم النفس والاجتماع", new DateTime(2026, 7, 2, 16, 15, 22, 820, DateTimeKind.Utc).AddTicks(2187) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "BirthDate", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NationalId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoUrl", "SecurityStamp", "UpdatedAt", "UserName" },
                values: new object[] { 1L, 0, null, new DateOnly(1990, 1, 1), "92cfbf6d-4931-4c6b-9321-56e21e5de86c", new DateTime(2026, 7, 2, 16, 15, 22, 736, DateTimeKind.Utc).AddTicks(9258), "mohammed.atef.abdelkader@gmail.com", true, "System", "M", "Administrator", false, null, 0L, "MOHAMMED.ATEF.ABDELKADER@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECguFO8hdBUbQoMvqimkES1sXQFffSrMHdxngOKnpYTa3a4ab0XIAISc2a/PJfdgog==", null, false, null, null, new DateTime(2026, 7, 2, 16, 15, 22, 736, DateTimeKind.Utc).AddTicks(9262), "admin" });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "ArabicName", "EnglishName", "ProvinceId" },
                values: new object[,]
                {
                    { 1L, "القاهرة", "Cairo", 1L },
                    { 2L, "الجيزة", "Giza", 1L },
                    { 3L, "الأسكندرية", "Alexandria", 2L },
                    { 4L, "الدقهلية", "Dakahlia", 3L },
                    { 5L, "البحر الأحمر", "Red Sea", 6L },
                    { 6L, "البحيرة", "Beheira", 2L },
                    { 7L, "الفيوم", "Fayoum", 5L },
                    { 8L, "الغربية", "Gharbiya", 3L },
                    { 9L, "الإسماعلية", "Ismailia", 4L },
                    { 10L, "المنوفية", "Menofia", 3L },
                    { 11L, "المنيا", "Minya", 5L },
                    { 12L, "القليوبية", "Qaliubiya", 1L },
                    { 13L, "الوادي الجديد", "New Valley", 6L },
                    { 14L, "السويس", "Suez", 4L },
                    { 15L, "شمال سيناء", "North Sinai", 4L },
                    { 16L, "جنوب سيناء", "South Sinai", 4L },
                    { 17L, "بني سويف", "Beni Suef", 5L },
                    { 18L, "بورسعيد", "Port Said", 6L },
                    { 19L, "دمياط", "Damietta", 3L },
                    { 20L, "الشرقية", "Sharkia", 4L },
                    { 21L, "جنوب سيناء", "South Sinai", 4L },
                    { 22L, "كفر الشيخ", "Kafr Al Sheikh", 3L },
                    { 23L, "مطروح", "Matrouh", 2L },
                    { 24L, "الأقصر", "Luxor", 7L },
                    { 25L, "قنا", "Qena", 7L },
                    { 26L, "شمال سيناء", "North Sinai", 4L },
                    { 27L, "سوهاج", "Sohag", 7L }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "ArabicName", "EnglishName", "GovernorateId" },
                values: new object[] { 1L, "Area 1", "Area 1", 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_GovernorateId",
                table: "Areas",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySlots_AreaId",
                table: "AvailabilitySlots",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySlots_EducationStageId",
                table: "AvailabilitySlots",
                column: "EducationStageId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySlots_EducationSystemId",
                table: "AvailabilitySlots",
                column: "EducationSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilitySlots_TeacherId",
                table: "AvailabilitySlots",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AvailabilitySlotId",
                table: "Bookings",
                column: "AvailabilitySlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TeacherId",
                table: "Bookings",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_TeacherId",
                table: "Favorites",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorates_ProvinceId",
                table: "Governorates",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TeacherId",
                table: "Reviews",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_AreaId",
                table: "UserAddresses",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "AvailabilitySlots");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "EducationStages");

            migrationBuilder.DropTable(
                name: "EducationSystems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Governorates");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
