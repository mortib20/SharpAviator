﻿// <auto-generated />
using System;
using Aviator.Main.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aviator.Main.Migrations
{
    [DbContext(typeof(AviatorContext))]
    [Migration("20240113144758_FixedWrongJaeroProto")]
    partial class FixedWrongJaeroProto
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Aviator.Main.Models.Endpoint", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("Decoder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Protocol")
                        .HasColumnType("INTEGER");

                    b.HasKey("Guid");

                    b.ToTable("Outputs");

                    b.HasData(
                        new
                        {
                            Guid = new Guid("0efa2d19-de65-45bd-a3a8-1305afd6cb26"),
                            Address = "feed.airframes.io",
                            Decoder = 0,
                            Port = 5552,
                            Protocol = 1
                        },
                        new
                        {
                            Guid = new Guid("2f48dcc1-190b-4c94-91d6-53900464e8e9"),
                            Address = "feed.airframes.io",
                            Decoder = 1,
                            Port = 5556,
                            Protocol = 0
                        },
                        new
                        {
                            Guid = new Guid("24cd028d-4202-4177-b673-75ccf6df2139"),
                            Address = "feed.airframes.io",
                            Decoder = 4,
                            Port = 5550,
                            Protocol = 1
                        },
                        new
                        {
                            Guid = new Guid("791e21c8-0763-4dc4-94bf-8241a24d8bec"),
                            Address = "feed.airframes.io",
                            Decoder = 2,
                            Port = 5571,
                            Protocol = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}