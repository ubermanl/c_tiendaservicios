﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicios.api.CarritoCompras.Persistencia;

namespace TiendaServicios.api.CarritoCompras.Migrations
{
    [DbContext(typeof(ContextoCarrito))]
    partial class ContextoCarritoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TiendaServicios.api.CarritoCompras.Aplicacion.Carrito", b =>
                {
                    b.Property<int>("CarritoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CarritoId");

                    b.ToTable("Carrito");
                });

            modelBuilder.Entity("TiendaServicios.api.CarritoCompras.Aplicacion.CarritoDetalle", b =>
                {
                    b.Property<int>("CarritoDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CarritoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProductoId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CarritoDetalleId");

                    b.HasIndex("CarritoId");

                    b.ToTable("CarritoDetalle");
                });

            modelBuilder.Entity("TiendaServicios.api.CarritoCompras.Aplicacion.CarritoDetalle", b =>
                {
                    b.HasOne("TiendaServicios.api.CarritoCompras.Aplicacion.Carrito", "Carrito")
                        .WithMany("ListaDetalle")
                        .HasForeignKey("CarritoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}