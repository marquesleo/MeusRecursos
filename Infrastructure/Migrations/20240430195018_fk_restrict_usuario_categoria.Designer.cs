﻿// <auto-generated />
using System;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ContextBase))]
    [Migration("20240430195018_fk_restrict_usuario_categoria")]
    partial class fk_restrict_usuario_categoria
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                        .HasColumnType("timestamp with time zone")
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
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("text");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("timestamp with time zone");

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

                    b.Property<Guid?>("Categoria_Id")
                        .HasColumnType("uuid")
                        .HasColumnName("categoria");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("descricao");

                    b.Property<DateTime>("DtAtualizacao")
                        .HasColumnType("timestamp with time zone")
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

                    b.HasIndex("Categoria_Id");

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
                        .OnDelete(DeleteBehavior.Restrict)
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
                    b.HasOne("Domain.Prioridades.Entities.Categoria", "Categoria")
                        .WithMany("Senhas")
                        .HasForeignKey("Categoria_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Prioridades.Entities.Usuario", "Usuario")
                        .WithOne("senha")
                        .HasForeignKey("Domain.Prioridades.Entities.Senha", "Usuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Categoria", b =>
                {
                    b.Navigation("Senhas");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Senha", b =>
                {
                    b.Navigation("ContadorDeSenha");
                });

            modelBuilder.Entity("Domain.Prioridades.Entities.Usuario", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("categoria");

                    b.Navigation("senha");
                });
#pragma warning restore 612, 618
        }
    }
}
