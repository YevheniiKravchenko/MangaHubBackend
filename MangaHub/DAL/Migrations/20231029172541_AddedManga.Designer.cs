﻿// <auto-generated />
using System;
using DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DbContextBase))]
    [Migration("20231029172541_AddedManga")]
    partial class AddedManga
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Chapter", b =>
                {
                    b.Property<Guid>("ChapterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ChapterNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MangaId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Scans")
                        .HasColumnType("bytea");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("ChapterId");

                    b.HasIndex("MangaId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("Domain.Models.Manga", b =>
                {
                    b.Property<Guid>("MangaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("CoverImage")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("LastUpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("MangaId");

                    b.ToTable("Mangas");
                });

            modelBuilder.Entity("Domain.Models.Rating", b =>
                {
                    b.Property<Guid>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MangaId")
                        .HasColumnType("uuid");

                    b.Property<byte>("Mark")
                        .HasColumnType("smallint");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RatingId");

                    b.HasIndex("MangaId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Domain.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Models.ResetPasswordToken", b =>
                {
                    b.Property<Guid>("ResetPasswordTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ResetPasswordTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("ResetPasswordTokens");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsAdmin = true,
                            Login = "Admin",
                            PasswordHash = "$2a$10$WkrWKFdubfRwcY4MjdFELui7Dh8r3ykAvDYOQPvQud0vPlxFHVen.",
                            PasswordSalt = "d!W2~4~zI{wq:l<p",
                            RegistrationDate = new DateTime(2023, 10, 29, 17, 25, 41, 525, DateTimeKind.Utc).AddTicks(2523)
                        });
                });

            modelBuilder.Entity("Domain.Models.UserProfile", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ShowConfidentialInformation")
                        .HasColumnType("boolean");

                    b.HasKey("UserId");

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Avatar = new byte[0],
                            BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Main administrator of the service",
                            Email = "admin@mangahub.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            PhoneNumber = "0505050505",
                            ShowConfidentialInformation = false
                        });
                });

            modelBuilder.Entity("Domain.Models.Chapter", b =>
                {
                    b.HasOne("Domain.Models.Manga", "Manga")
                        .WithMany("Chapters")
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manga");
                });

            modelBuilder.Entity("Domain.Models.Rating", b =>
                {
                    b.HasOne("Domain.Models.Manga", "Manga")
                        .WithMany("Ratings")
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Manga");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.RefreshToken", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.ResetPasswordToken", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("ResetPasswordTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.UserProfile", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Domain.Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Manga", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("RefreshTokens");

                    b.Navigation("ResetPasswordTokens");

                    b.Navigation("UserProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
