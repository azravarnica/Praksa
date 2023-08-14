﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieAPI.Models;

#nullable disable

namespace MovieAPI.Migrations
{
    [DbContext(typeof(APIDbContext))]
    partial class APIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieAPI.Models.Movie", b =>
                {
                    b.Property<int>("ImdbID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImdbID"));

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Rated")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Released")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ImdbID");

                    b.ToTable("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}