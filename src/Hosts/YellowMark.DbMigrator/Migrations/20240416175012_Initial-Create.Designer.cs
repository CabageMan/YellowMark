﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using YellowMark.DbMigrator.DatabaseContext;

#nullable disable

namespace YellowMark.DbMigrator.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    [Migration("20240416175012_Initial-Create")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("YellowMark.Domain.Ads.Entity.Ad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SubcategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("SubcategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Ads", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Categories.Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Comments.Entity.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Currencies.Entity.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AlphabeticCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumericCode")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Currencies", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Files.Entity.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Content")
                        .HasColumnType("bytea");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Subcategories.Entity.Subcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategories", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Users.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("YellowMark.Domain.Ads.Entity.Ad", b =>
                {
                    b.HasOne("YellowMark.Domain.Currencies.Entity.Currency", "Currency")
                        .WithMany("Ads")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("YellowMark.Domain.Subcategories.Entity.Subcategory", "Subcategory")
                        .WithMany("Ads")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("YellowMark.Domain.Users.Entity.User", "User")
                        .WithMany("Ads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Subcategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YellowMark.Domain.Comments.Entity.Comment", b =>
                {
                    b.HasOne("YellowMark.Domain.Ads.Entity.Ad", "Ad")
                        .WithMany("Comments")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YellowMark.Domain.Users.Entity.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YellowMark.Domain.Files.Entity.File", b =>
                {
                    b.HasOne("YellowMark.Domain.Ads.Entity.Ad", "Ad")
                        .WithMany("Files")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("YellowMark.Domain.Subcategories.Entity.Subcategory", b =>
                {
                    b.HasOne("YellowMark.Domain.Categories.Entity.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("YellowMark.Domain.Ads.Entity.Ad", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("YellowMark.Domain.Categories.Entity.Category", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("YellowMark.Domain.Currencies.Entity.Currency", b =>
                {
                    b.Navigation("Ads");
                });

            modelBuilder.Entity("YellowMark.Domain.Subcategories.Entity.Subcategory", b =>
                {
                    b.Navigation("Ads");
                });

            modelBuilder.Entity("YellowMark.Domain.Users.Entity.User", b =>
                {
                    b.Navigation("Ads");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
