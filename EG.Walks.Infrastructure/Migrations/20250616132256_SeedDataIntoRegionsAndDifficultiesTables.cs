using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EG.Walks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataIntoRegionsAndDifficultiesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("08ad254f-4369-4654-80dd-ffce9e1ca07f"), "Medium" },
                    { new Guid("849cb121-a3d5-4d6d-a27b-e9b1623e9237"), "Hard" },
                    { new Guid("d9801dca-5af2-446f-9478-580dd32414ff"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("249ddc21-7b4a-436c-8d26-421dbdd66daf"), "S01", "South Sinai", "https://example.com/images/southsinai.jpg" },
                    { new Guid("6583823e-a0ff-4fd7-9d4c-92dcbc6f4638"), "R01", "Red Sea", "https://example.com/images/redsea.jpg" },
                    { new Guid("744a5216-4c28-4235-8d52-9edb90073da0"), "A01", "Alexandria", "https://example.com/images/alexandria.jpg" },
                    { new Guid("8d117c3d-47a3-4726-965b-d1b073168bb6"), "S02", "Sharqia", "https://example.com/images/sharqia.jpg" },
                    { new Guid("97a16dca-646b-4f3d-bd42-9d684a8ca1fd"), "G01", "Giza", "https://example.com/images/giza.jpg" },
                    { new Guid("a7b29cd6-3ca8-4a65-8f5b-a7962e0ab63b"), "L01", "Luxor", "https://example.com/images/luxor.jpg" },
                    { new Guid("ad5bdb93-c574-4b9d-8075-d9035704969c"), "C01", "Cairo", "https://example.com/images/cairo.jpg" },
                    { new Guid("c4d3ff86-415b-40fe-8681-dc058d5de9d7"), "Q01", "Quina", "https://example.com/images/quina.jpg" },
                    { new Guid("eff6c0e2-8bec-4ae3-97d0-5d096d82b337"), "S03", "Sohage", "https://example.com/images/sohage.jpg" },
                    { new Guid("fb1394dc-9e33-41c1-9a9a-a3faf8eac4e6"), "A02", "Aswan", "https://example.com/images/aswan.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("08ad254f-4369-4654-80dd-ffce9e1ca07f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("849cb121-a3d5-4d6d-a27b-e9b1623e9237"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d9801dca-5af2-446f-9478-580dd32414ff"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("249ddc21-7b4a-436c-8d26-421dbdd66daf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6583823e-a0ff-4fd7-9d4c-92dcbc6f4638"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("744a5216-4c28-4235-8d52-9edb90073da0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8d117c3d-47a3-4726-965b-d1b073168bb6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("97a16dca-646b-4f3d-bd42-9d684a8ca1fd"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a7b29cd6-3ca8-4a65-8f5b-a7962e0ab63b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ad5bdb93-c574-4b9d-8075-d9035704969c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c4d3ff86-415b-40fe-8681-dc058d5de9d7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("eff6c0e2-8bec-4ae3-97d0-5d096d82b337"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fb1394dc-9e33-41c1-9a9a-a3faf8eac4e6"));
        }
    }
}
