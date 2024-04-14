﻿// <auto-generated />
using System;
using FilmManager.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FilmManager.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240410080753_InitialCreation")]
    partial class InitialCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FilmManager.DAL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("parent_category_id");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("FilmManager.DAL.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Release")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("films");
                });

            modelBuilder.Entity("FilmManager.DAL.Models.FilmCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<int>("FilmId")
                        .HasColumnType("int")
                        .HasColumnName("film_id");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FilmId");

                    b.ToTable("film_categories");
                });

            modelBuilder.Entity("FilmManager.DAL.Models.Category", b =>
                {
                    b.HasOne("FilmManager.DAL.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("FilmManager.DAL.Models.FilmCategory", b =>
                {
                    b.HasOne("FilmManager.DAL.Models.Category", "Category")
                        .WithMany("FilmCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmManager.DAL.Models.Film", "Film")
                        .WithMany("FilmCategories")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("FilmManager.DAL.Models.Category", b =>
                {
                    b.Navigation("FilmCategories");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("FilmManager.DAL.Models.Film", b =>
                {
                    b.Navigation("FilmCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
