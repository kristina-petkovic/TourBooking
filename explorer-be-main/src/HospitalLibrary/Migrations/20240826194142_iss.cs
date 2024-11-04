using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class iss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Issues",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$dB4w.m2zZacE0EBFbBot0eA5Sd2rwM8qk3Afa9iLKaqPV7cpeGqJe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$1XgxGB95UAvyNFax/DynHOtHN.9Ix/Jxn6mShFSLQgX2QM1EanMjS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2b$10$H71nfaT90gSZqvPS4H1M4utE3NN310F1jCpF7P8pRQcK6VMrQyIZi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Issues");

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
    }
}
