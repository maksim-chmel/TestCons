using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestCons.Migrations
{
    /// <inheritdoc />
    public partial class FirstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxiTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpep_pickup_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tpep_dropoff_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    passenger_count = table.Column<int>(type: "integer", nullable: false),
                    trip_distance = table.Column<decimal>(type: "numeric", nullable: false),
                    store_and_fwd_flag = table.Column<string>(type: "text", nullable: false),
                    PULocationID = table.Column<int>(type: "integer", nullable: false),
                    DOLocationID = table.Column<int>(type: "integer", nullable: false),
                    fare_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    tip_amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiTrips", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxiTrips_PULocationID",
                table: "TaxiTrips",
                column: "PULocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiTrips_tpep_pickup_datetime_tpep_dropoff_datetime",
                table: "TaxiTrips",
                columns: new[] { "tpep_pickup_datetime", "tpep_dropoff_datetime" });

            migrationBuilder.CreateIndex(
                name: "IX_TaxiTrips_trip_distance",
                table: "TaxiTrips",
                column: "trip_distance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxiTrips");
        }
    }
}
