using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySql.Data.EntityFrameworkCore.Metadata;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    [DbContext(typeof(ContextoCarrito))]

    public class CarritoContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoCompra", b =>
            {
                b.Property<int>("CarritoCompraId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime?>("FechaCreacion")
                    .HasColumnType("timestamp without time zone");

                b.HasKey("CarritoCompraId");

                b.ToTable("CarritoCompra");
            });

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoCompraDetalle", b =>
            {
                b.Property<int>("CarritoCompraDetalleId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .ValueGeneratedOnAdd();

                b.Property<int>("CarritoCompraId")
                    .HasColumnType("integer");

                b.Property<DateTime?>("FechaCreacion")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("ProductoSeleccionado")
                    .HasColumnType("text");

                b.HasKey("CarritoCompraDetalleId");

                b.HasIndex("CarritoCompraId");

                b.ToTable("CarritoCompraDetalle");
            });

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoCompraDetalle", b =>
            {
                b.HasOne("TiendaServicios.Api.CarritoCompra.Modelo.CarritoCompra", "CarritoCompra")
                    .WithMany("ListaDetalle")
                    .HasForeignKey("CarritoCompraId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("CarritoCompra");
            });

            modelBuilder.Entity("TiendaServicios.Api.CarritoCompra.Modelo.CarritoCompra", b =>
            {
                b.Navigation("ListaDetalle");
            });
#pragma warning restore 612, 618
        }
    }
}


