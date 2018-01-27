﻿// <auto-generated />
using BasicApiCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BasicApiCore.Migrations
{
    [DbContext(typeof(CastleContext))]
    [Migration("20180127144238_CastleDetailMigration")]
    partial class CastleDetailMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BasicApiCore.Entities.Castle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Castle");
                });

            modelBuilder.Entity("BasicApiCore.Entities.CastleDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CastleId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CastleId");

                    b.ToTable("CastleDetails");
                });

            modelBuilder.Entity("BasicApiCore.Entities.CastleDetails", b =>
                {
                    b.HasOne("BasicApiCore.Entities.Castle", "Castle")
                        .WithMany("CastleDetails")
                        .HasForeignKey("CastleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}