using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class n : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Issues");

            migrationBuilder.UpdateData(
                table: "Purchases",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$y4ptuf.RPNRWCem0WuikG.3i1aP/7rWfJ1IRvHNKzTdmeXByI5qV2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$HxR60ve1LVSsTKPMu0RH/.gN8VTlMNdApUL2YCFHApV5FmpJYl1mC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2b$10$xPAGwK.VH8k9J3mml87jdexKNxD6QD66ov5PmH9K8EkJ6BL7HopH2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Issues",
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
        }
    }
}
