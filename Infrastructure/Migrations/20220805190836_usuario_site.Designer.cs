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
    [Migration("20220805190836_usuario_site")]
    partial class usuario_site
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

            modelBuilder.Entity("Domain.Prioridades.Entities.Senha", b =>
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

                    b.Property<DateTime>("DtAtualizacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_atualizacao");

                    b.Property<byte[]>("Imagem")
                        .HasColumnType("bytea")
                        .HasColumnName("imagem");

                    b.Property<string>("NomeImagem")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("nomeimagem");

                    b.Property<string>("Observacao")
                        .HasColumnType("text")
                        .HasColumnName("observacao");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("password");

                    b.Property<string>("Site")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("site");

                    b.Property<string>("UrlImageSite")
                        .HasColumnType("text")
                        .HasColumnName("url_img_site");

                    b.Property<Guid>("Usuario_Id")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario");

                    b.Property<string>("Usuario_Site")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("usuario_site");

                    b.HasKey("Id");

                    b.HasIndex("Usuario_Id");

                    b.ToTable("senha", "personal");
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

            modelBuilder.Entity("Domain.Prioridades.Entities.Senha", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Usuario", "Usuario")
                        .WithOne("senha")
                        .HasForeignKey("Domain.Prioridades.Entities.Senha", "Usuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Usuario", b =>
                {
                    b.Navigation("senha");
                });
#pragma warning restore 612, 618
        }
    }
}
