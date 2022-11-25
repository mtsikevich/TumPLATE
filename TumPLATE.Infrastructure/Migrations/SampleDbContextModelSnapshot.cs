﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TumPLATE.Infrastructure.Persistence;

#nullable disable

namespace TumPLATE.Infrastructure.Migrations
{
    [DbContext(typeof(SampleDbContext))]
    partial class SampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TumPLATE.Domain.Tree.Fruit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int?>("TreeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TreeId");

                    b.ToTable("Fruit");
                });

            modelBuilder.Entity("TumPLATE.Domain.Tree.Tree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastFruitAddingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trees");
                });

            modelBuilder.Entity("TumPLATE.Domain.Tree.Fruit", b =>
                {
                    b.HasOne("TumPLATE.Domain.Tree.Tree", null)
                        .WithMany("Fruits")
                        .HasForeignKey("TreeId");
                });

            modelBuilder.Entity("TumPLATE.Domain.Tree.Tree", b =>
                {
                    b.Navigation("Fruits");
                });
#pragma warning restore 612, 618
        }
    }
}
