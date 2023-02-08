﻿// <auto-generated />
using BackFinal.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackFinal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230207104753_AddIndexSlider")]
    partial class AddIndexSlider
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("BackFinal.Models.IndexSlider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TextBottom")
                        .HasColumnType("TEXT");

                    b.Property<string>("TextHead")
                        .HasColumnType("TEXT");

                    b.Property<string>("TextMiddle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IndexSliders");
                });
#pragma warning restore 612, 618
        }
    }
}
