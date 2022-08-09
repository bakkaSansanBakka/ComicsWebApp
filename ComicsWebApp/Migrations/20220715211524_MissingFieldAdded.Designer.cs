﻿// <auto-generated />
using System;
using ComicsWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComicsWebApp.Migrations
{
    [DbContext(typeof(ComicsDbContext))]
    [Migration("20220715211524_MissingFieldAdded")]
    partial class MissingFieldAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ComicsComicsGenre", b =>
                {
                    b.Property<int>("ComicsId")
                        .HasColumnType("int");

                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.HasKey("ComicsId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("ComicsComicsGenre");
                });

            modelBuilder.Entity("ComicsWebApp.Models.Comics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvailabilityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Cover")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("CoverType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PagesNumber")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("PublicationFormat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfPublisihing")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Comics");
                });

            modelBuilder.Entity("ComicsWebApp.Models.ComicsGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComicsGenres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreName = "Detective"
                        },
                        new
                        {
                            Id = 2,
                            GenreName = "Historical"
                        },
                        new
                        {
                            Id = 3,
                            GenreName = "Science Fiction"
                        },
                        new
                        {
                            Id = 4,
                            GenreName = "Educational"
                        },
                        new
                        {
                            Id = 5,
                            GenreName = "Adventure"
                        },
                        new
                        {
                            Id = 6,
                            GenreName = "Romantic"
                        },
                        new
                        {
                            Id = 7,
                            GenreName = "Horror"
                        },
                        new
                        {
                            Id = 8,
                            GenreName = "Fantasy"
                        },
                        new
                        {
                            Id = 9,
                            GenreName = "Humor"
                        });
                });

            modelBuilder.Entity("ComicsWebApp.Models.ComicsPages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComicsId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComicsId");

                    b.ToTable("ComicsPages");
                });

            modelBuilder.Entity("ComicsComicsGenre", b =>
                {
                    b.HasOne("ComicsWebApp.Models.Comics", null)
                        .WithMany()
                        .HasForeignKey("ComicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicsWebApp.Models.ComicsGenre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComicsWebApp.Models.ComicsPages", b =>
                {
                    b.HasOne("ComicsWebApp.Models.Comics", "Comics")
                        .WithMany("Pages")
                        .HasForeignKey("ComicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comics");
                });

            modelBuilder.Entity("ComicsWebApp.Models.Comics", b =>
                {
                    b.Navigation("Pages");
                });
#pragma warning restore 612, 618
        }
    }
}
