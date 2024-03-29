﻿// <auto-generated />
using System;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ContextBase))]
    [Migration("20220624155225_FeitoColumn")]
    partial class FeitoColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Domain.Prioridades.Entities.Prioridade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("ativo");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("descricao");

                    b.Property<bool>("Feito")
                        .HasColumnType("boolean")
                        .HasColumnName("feito");

                    b.Property<int>("Valor")
                        .HasColumnType("integer")
                        .HasColumnName("valor");

                    b.Property<Guid?>("usuario")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("usuario");

                    b.ToTable("prioridade", "personal");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("usuario", "personal");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Prioridade", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuario");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
