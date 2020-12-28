using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    public class _20201223165130_MigrationMySqlInicial
    {
        public partial class MigrationMySqlInicial : Migration
        {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(
                    name: "CarritoCompra",
                    columns: table => new
                    {
                        CarritoCompraId = table.Column<int>(nullable: false)
                            .Annotation("MySql: ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        FechaCreacion = table.Column<DateTime>(nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_CarritoCompra", x => x.CarritoCompraId);
                    });

                migrationBuilder.CreateTable(
                    name: "CarritoCompraDetalle",
                    columns: table => new
                    {
                        CarritoCompraDetalleId = table.Column<int>(nullable: false)
                           .Annotation("MySql: ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        FechaCreacion = table.Column<DateTime>(nullable: true),
                        ProductoSeleccionado = table.Column<string>(nullable: true),
                        CarritoCompraId = table.Column<int>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_CarritoCompraDetalle", x => x.CarritoCompraDetalleId);
                        table.ForeignKey(
                            name: "FK_CarritoCompraDetalle_CarritoCompra_CarritoCompraId",
                            column: x => x.CarritoCompraId,
                            principalTable: "CarritoCompra",
                            principalColumn: "CarritoCompraId",
                            onDelete: ReferentialAction.Cascade
                            );
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_CarritoCompraDetalle_CarritoCompraId",
                    table: "CarritoCompraDetalle",
                    column: "CarritoCompraId"
                    );
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropTable(
                    name: "CarritoCompraDetalle");

                migrationBuilder.DropTable(
                    name: "CarritoCompra");
            }
        }
    }
}
