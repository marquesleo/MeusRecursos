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
    [Migration("20240102140534_ColunaDtAcesso")]
    partial class ColunaDtAcesso
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Domain.Prioridades.Entities.Categoria", b =>
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

                    b.Property<byte[]>("Imagem")
                        .HasColumnType("bytea")
                        .HasColumnName("imagem");

                    b.Property<string>("NomeImagem")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("nomeimagem");

                    b.Property<string>("UrlImageSite")
                        .HasColumnType("text")
                        .HasColumnName("url_img_site");

                    b.Property<Guid>("Usuario_Id")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario");

                    b.HasKey("Id");

                    b.HasIndex("Usuario_Id");

                    b.ToTable("categoria", "personal");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.ContadorDeSenha", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Contador")
                        .HasColumnType("integer")
                        .HasColumnName("contador");

                    b.Property<DateTime>("DataDeAcesso")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DataDeAcesso");

                    b.Property<Guid>("SenhaId")
                        .HasColumnType("uuid")
                        .HasColumnName("senha");

                    b.HasKey("Id");

                    b.HasIndex("SenhaId");

                    b.ToTable("contadorsenha", "personal");
                });

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

            modelBuilder.Entity("Domain.Prioridades.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("text");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("refreshtoken", "personal");
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

            modelBuilder.Entity("Domain.Prioridades.Entities.Categoria", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Usuario", "Usuario")
                        .WithOne("categoria")
                        .HasForeignKey("Domain.Prioridades.Entities.Categoria", "Usuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.ContadorDeSenha", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Senha", "Senha")
                        .WithOne("ContadorDeSenha")
                        .HasForeignKey("Domain.Prioridades.Entities.ContadorDeSenha", "SenhaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Senha");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Prioridade", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Prioridades.Entities.Usuario", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Domain.Prioridades.Entities.Senha", b =>
                {
                    b.Navigation("ContadorDeSenha");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Usuario", b =>
                {
                    b.Navigation("categoria");

                    b.Navigation("RefreshTokens");

                    b.Navigation("senha");
                });
#pragma warning restore 612, 618
        }
    }
}
