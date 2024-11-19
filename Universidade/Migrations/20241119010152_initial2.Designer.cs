﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Universidade.Data;

#nullable disable

namespace Universidade.Migrations
{
    [DbContext(typeof(UniversidadeContext))]
    [Migration("20241119010152_initial2")]
    partial class initial2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlunoDisciplina", b =>
                {
                    b.Property<int>("AlunosId")
                        .HasColumnType("int");

                    b.Property<int>("DisciplinasId")
                        .HasColumnType("int");

                    b.HasKey("AlunosId", "DisciplinasId");

                    b.HasIndex("DisciplinasId");

                    b.ToTable("AlunoDisciplina");

                    b.HasData(
                        new
                        {
                            AlunosId = 2,
                            DisciplinasId = 1
                        },
                        new
                        {
                            AlunosId = 1,
                            DisciplinasId = 2
                        });
                });

            modelBuilder.Entity("Universidade.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date");

                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alunos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Data = new DateOnly(2023, 9, 21),
                            Matricula = 202314593,
                            Nome = "Maria Lopes"
                        },
                        new
                        {
                            Id = 2,
                            Data = new DateOnly(2023, 10, 22),
                            Matricula = 202314956,
                            Nome = "Joao Carlos"
                        });
                });

            modelBuilder.Entity("Universidade.Models.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Disciplinas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            DataRegistro = new DateTime(2024, 11, 18, 22, 1, 51, 696, DateTimeKind.Local).AddTicks(8468),
                            Descricao = "Traga as palavras",
                            Nome = "Profeta",
                            ProfessorId = 1
                        },
                        new
                        {
                            Id = 2,
                            Ativo = true,
                            DataRegistro = new DateTime(2024, 11, 18, 22, 1, 51, 696, DateTimeKind.Local).AddTicks(8470),
                            Descricao = "Testemunhe o mundo",
                            Nome = "Testemunha",
                            ProfessorId = 1
                        });
                });

            modelBuilder.Entity("Universidade.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date");

                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Data = new DateOnly(2013, 1, 20),
                            Matricula = 20231214,
                            Nome = "Jon Cleber"
                        },
                        new
                        {
                            Id = 2,
                            Data = new DateOnly(2013, 1, 20),
                            Matricula = 20231215,
                            Nome = "Leo John"
                        });
                });

            modelBuilder.Entity("AlunoDisciplina", b =>
                {
                    b.HasOne("Universidade.Models.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Universidade.Models.Disciplina", null)
                        .WithMany()
                        .HasForeignKey("DisciplinasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Universidade.Models.Disciplina", b =>
                {
                    b.HasOne("Universidade.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });
#pragma warning restore 612, 618
        }
    }
}
