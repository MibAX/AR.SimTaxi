using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AR.SimTaxi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Car_rename_prodductionYear_productionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionYear",
                table: "Cars",
                newName: "ProductionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionDate",
                table: "Cars",
                newName: "ProductionYear");
        }
    }
}
