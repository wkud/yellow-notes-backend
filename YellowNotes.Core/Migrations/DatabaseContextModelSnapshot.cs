﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YellowNotes.Core;

namespace YellowNotes.Core.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("YellowNotes.Core.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .IsUnicode(false);

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .IsUnicode(false);

                    b.HasKey("Email");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
