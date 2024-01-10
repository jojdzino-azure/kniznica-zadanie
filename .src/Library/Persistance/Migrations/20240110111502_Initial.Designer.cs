﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistance;

#nullable disable

namespace Persistance.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240110111502_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.BookEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InternalComment")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BorrowingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("BorrowedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("BorrowedById")
                        .HasColumnType("integer");

                    b.Property<int>("BorrowedForDays")
                        .HasColumnType("integer");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("Notified")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReturnedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("BorrowedById");

                    b.ToTable("Borrowing", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Guid");

                    b.HasIndex("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1996, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc),
                            Email = "jozef.schneider.95@gmail.com",
                            FirstName = "Jozef",
                            Guid = new Guid("7fdeb6bd-8fdc-4618-8284-a5e2e1e44919"),
                            LastName = "Schneider",
                            RegisteredAt = new DateTime(2022, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1998, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc),
                            Email = "somerandommail@gmail.com",
                            FirstName = "Daniel",
                            Guid = new Guid("b2572760-2335-4ab4-a3b9-173a1ab3f85e"),
                            LastName = "Vidlicka",
                            RegisteredAt = new DateTime(2023, 8, 24, 9, 48, 6, 984, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("Domain.Entities.BorrowingEntity", b =>
                {
                    b.HasOne("Domain.Entities.BookEntity", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEntity", "BorrowedBy")
                        .WithMany()
                        .HasForeignKey("BorrowedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("BorrowedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
