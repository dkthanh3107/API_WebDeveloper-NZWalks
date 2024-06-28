using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyNZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataDifficulyandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3c09e1ab-2d79-4fe6-830d-84a645b38d4e"), "Medium" },
                    { new Guid("8009e0ea-5788-4d46-ac72-a4664affb9f1"), "Hard" },
                    { new Guid("84580491-59d9-403a-abc7-aa3e5d55c030"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("31e513ad-ea02-45f2-8e14-ec15d71c1139"), "NTL", "Northland", null },
                    { new Guid("3b2322cb-73a9-4820-aafa-a9ae8f0aed2e"), "BOP", "Bay Of Plenty", null },
                    { new Guid("5c2b6c29-20b3-4614-bcb2-21368ff4d956"), "STL", "Southland", null },
                    { new Guid("60dda257-b71d-4076-bb49-0640996de186"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("7f14bd2a-32f9-4fbf-b55c-607640a93b34"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("ef02783e-f79a-4703-b652-4710ebedabd0"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3c09e1ab-2d79-4fe6-830d-84a645b38d4e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8009e0ea-5788-4d46-ac72-a4664affb9f1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("84580491-59d9-403a-abc7-aa3e5d55c030"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("31e513ad-ea02-45f2-8e14-ec15d71c1139"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3b2322cb-73a9-4820-aafa-a9ae8f0aed2e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5c2b6c29-20b3-4614-bcb2-21368ff4d956"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("60dda257-b71d-4076-bb49-0640996de186"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7f14bd2a-32f9-4fbf-b55c-607640a93b34"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ef02783e-f79a-4703-b652-4710ebedabd0"));
        }
    }
}
