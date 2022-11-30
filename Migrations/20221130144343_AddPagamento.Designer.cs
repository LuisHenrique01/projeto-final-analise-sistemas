﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto_Final.Models;

#nullable disable

namespace Projeto_Final.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20221130144343_AddPagamento")]
    partial class AddPagamento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Projeto_Final.Models.Boleto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("codigo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Boleto");
                });

            modelBuilder.Entity("Projeto_Final.Models.CartaoCredito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("numero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CartaoCredito");
                });

            modelBuilder.Entity("Projeto_Final.Models.Consumidor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Consumidor");
                });

            modelBuilder.Entity("Projeto_Final.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConsumidorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsumidorId");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("Projeto_Final.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("PedidoId")
                        .HasColumnType("int");

                    b.Property<double>("ValorProduto")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Projeto_Final.Models.Pedido", b =>
                {
                    b.HasOne("Projeto_Final.Models.Consumidor", "Consumidor")
                        .WithMany()
                        .HasForeignKey("ConsumidorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consumidor");
                });

            modelBuilder.Entity("Projeto_Final.Models.Produto", b =>
                {
                    b.HasOne("Projeto_Final.Models.Pedido", null)
                        .WithMany("Produtos")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("Projeto_Final.Models.Pedido", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
