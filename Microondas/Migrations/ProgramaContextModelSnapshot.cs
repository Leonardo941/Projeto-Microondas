﻿// <auto-generated />
using Microondas.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Microondas.Migrations
{
    [DbContext(typeof(ProgramaContext))]
    partial class ProgramaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microondas.Models.Programa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alimento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Caractere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Customizado")
                        .HasColumnType("bit");

                    b.Property<string>("Instrucao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Potencia")
                        .HasColumnType("int");

                    b.Property<string>("Tempo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programas");
                });
#pragma warning restore 612, 618
        }
    }
}
