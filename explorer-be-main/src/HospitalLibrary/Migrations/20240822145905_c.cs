using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "CartItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Count",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Purchases",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2024, 8, 22, 16, 59, 4, 199, DateTimeKind.Local).AddTicks(5921));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$umqfF7AostMJqpguk6AX1u524hSvq9vbylnpCZuYrmFZMMT4baPTW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$PdnICnhfUuaOcur8o0ohoe/V9JwxfCm6T.72qGcKNYw6zUc2VzxQe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2b$10$GTKg9Dqc6/XOItIsNTfztOoG9Y5BFofxpXDShZ2em.vQRjcLWKY2O");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "Purchases",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2024, 8, 21, 19, 28, 6, 943, DateTimeKind.Local).AddTicks(1187));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$fLnMEI.2oRNma47FVqlVj.GxiMGzsUx9DAwA.zGNBFZrSi/AVYWYG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$dW.kpR/XKoR59eqe/2Yzfu0waEq9O0MzMpUdXTKc/OXIz7dL7JADi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2b$10$cng9obAG9XUf3UXLgEN20OsryKQcAUbJ9M/M3FtvuuNtHv4kPgJlS");
        }
    }
}
