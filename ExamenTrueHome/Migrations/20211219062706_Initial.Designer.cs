﻿// <auto-generated />
using System;
using ExamenTrueHome.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ExamenTrueHome.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211219062706_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ExamenTrueHome.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Property_Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Schedule")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("Update_At")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Property_Id");

                    b.HasIndex("Status_Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ExamenTrueHome.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("Disabled_At")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated_At")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Status_Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("ExamenTrueHome.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Done"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Cancel"
                        });
                });

            modelBuilder.Entity("ExamenTrueHome.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Activity_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Answers")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Activity_Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("ExamenTrueHome.Models.Activity", b =>
                {
                    b.HasOne("ExamenTrueHome.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("Property_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamenTrueHome.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("Status_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ExamenTrueHome.Models.Property", b =>
                {
                    b.HasOne("ExamenTrueHome.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("Status_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ExamenTrueHome.Models.Survey", b =>
                {
                    b.HasOne("ExamenTrueHome.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("Activity_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });
#pragma warning restore 612, 618
        }
    }
}