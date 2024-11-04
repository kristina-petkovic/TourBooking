using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TouristId = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterestTypeName = table.Column<int>(type: "integer", nullable: false),
                    TouristId = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    AdminId = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<int>(type: "integer", nullable: false),
                    TouristId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<int>(type: "integer", nullable: false),
                    TourName = table.Column<string>(type: "text", nullable: true),
                    TouristId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SoldToursCount = table.Column<int>(type: "integer", nullable: false),
                    TotalProfit = table.Column<double>(type: "double precision", nullable: false),
                    SalesIncreasePercentage = table.Column<string>(type: "text", nullable: true),
                    TopSellingTourId = table.Column<int>(type: "integer", nullable: false),
                    TopSellingTourCount = table.Column<int>(type: "integer", nullable: false),
                    LeastSellingTourId = table.Column<int>(type: "integer", nullable: false),
                    LeastSellingTourCount = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    TicketCount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    NoSalesInLastThreeMonths = table.Column<bool>(type: "boolean", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueCount = table.Column<int>(type: "integer", nullable: false),
                    Malicious = table.Column<bool>(type: "boolean", nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorPoints = table.Column<int>(type: "integer", nullable: false),
                    TopAuthor = table.Column<bool>(type: "boolean", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatusChange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ChangedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatusChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueStatusChange_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "Deleted", "TourId", "TouristId" },
                values: new object[] { 1, false, 1, 1 });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Deleted", "InterestTypeName", "TourId", "TouristId" },
                values: new object[,]
                {
                    { 1, false, 4, 0, 1 },
                    { 2, false, 0, 0, 1 },
                    { 3, false, 4, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "KeyPoints",
                columns: new[] { "Id", "Deleted", "Description", "Image", "Latitude", "Longitude", "Name", "Order", "TourId" },
                values: new object[,]
                {
                    { 1, false, "spomenik kulture", "spomenik3.jpg", 38.895099999999999, -77.0364, "kp ture 1", 1, 1 },
                    { 2, false, "spomenik kulture", "spomenik3.jpg", 39.895099999999999, -77.0364, "kp ture 1, drugi po redu", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Id", "AuthorId", "Deleted", "Price", "PurchaseDate", "TourId", "TourName", "TouristId" },
                values: new object[] { 1, 2, false, 10.0, new DateTime(2024, 8, 21, 19, 28, 6, 943, DateTimeKind.Local).AddTicks(1187), 1, "tura autora broj 1", 1 });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "AuthorId", "Deleted", "Description", "Difficulty", "Name", "NoSalesInLastThreeMonths", "Price", "Status", "TicketCount" },
                values: new object[,]
                {
                    { 1, 2, false, "prva tura", 0, "tura autora broj 1", false, 10, 1, 150 },
                    { 2, 2, false, "druga tura", 0, "tura autora broj 1", false, 10, 0, 150 },
                    { 3, 2, false, "treca tura", 2, "tura autora broj 1", false, 10, 1, 150 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthorPoints", "Blocked", "Deleted", "Email", "FirstName", "IssueCount", "LastName", "Malicious", "Password", "Role", "TopAuthor", "Username" },
                values: new object[,]
                {
                    { 1, 0, false, false, "kpetkovic18@gmail.com", "Kristina", 0, "Petkovic", false, "$2b$10$fLnMEI.2oRNma47FVqlVj.GxiMGzsUx9DAwA.zGNBFZrSi/AVYWYG", 0, false, "tourist1" },
                    { 2, 0, false, false, "kpetkovic18@gmail.com", "Bjanka", 0, "Tijodorovic", false, "$2b$10$dW.kpR/XKoR59eqe/2Yzfu0waEq9O0MzMpUdXTKc/OXIz7dL7JADi", 1, false, "author2" },
                    { 3, 0, false, false, "kpetkovic18@gmail.com", "Jelena", 0, "Petkovic", false, "$2b$10$cng9obAG9XUf3UXLgEN20OsryKQcAUbJ9M/M3FtvuuNtHv4kPgJlS", 2, false, "admin3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueStatusChange_IssueId",
                table: "IssueStatusChange",
                column: "IssueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "IssueStatusChange");

            migrationBuilder.DropTable(
                name: "KeyPoints");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
