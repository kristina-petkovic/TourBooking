using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class T : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Purchases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Purchases",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2024, 8, 22, 19, 16, 48, 644, DateTimeKind.Local).AddTicks(5634));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$yvoVI2N1YDsn4XIc07FjVOd2uRA7DOsjJ/kjLuPoD/blUyrNT3W9O");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$PAsomxwK/45hVLFf3R.tauK51.q52rGq1u7gQ/XWTjDvWlQdiYYmi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2b$10$yklAvqx98jOG6fq4UBaNbedozjiFTxNDkXqGb15UJ0xqAQiGMg8Sa");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_TourId",
                table: "CartItems",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Tours_TourId",
                table: "CartItems",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Tours_TourId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_TourId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Purchases");

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
    }
}
