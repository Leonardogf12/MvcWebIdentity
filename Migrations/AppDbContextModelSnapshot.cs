﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcWebIdentity.Context;

#nullable disable

namespace MvcWebIdentity.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MvcWebIdentity.Entities.Aluno", b =>
                {
                    b.Property<int>("ALUNOID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ALU_CURSO")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("ALU_EMAIL")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<int>("ALU_IDADE")
                        .HasColumnType("int");

                    b.Property<string>("ALU_NOME")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("ALUNOID");

                    b.ToTable("Alunos");
                });
#pragma warning restore 612, 618
        }
    }
}
