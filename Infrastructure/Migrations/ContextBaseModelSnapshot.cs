﻿// <auto-generated />
using System;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ContextBase))]
    partial class ContextBaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.Prioridades.Entities.Prioridade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnName("ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descricao")
                        .HasColumnName("descricao")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Valor")
                        .HasColumnName("valor")
                        .HasColumnType("integer");

                    b.Property<Guid?>("usuario")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("usuario");

                    b.ToTable("prioridade","personal");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("usuario","personal");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Prioridade", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
