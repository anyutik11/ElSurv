﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web.Db;

#nullable disable

namespace web.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230423110406_AnswerNull")]
    partial class AnswerNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("web.Db.Answer", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(22)
                        .HasColumnType("char(22)");

                    b.Property<string>("companyId")
                        .IsRequired()
                        .HasColumnType("char(22)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("guestId")
                        .HasColumnType("char(22)");

                    b.Property<int?>("mark")
                        .HasColumnType("int");

                    b.Property<string>("phone")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("reviewId")
                        .IsRequired()
                        .HasColumnType("char(22)");

                    b.Property<string>("text1")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("text2")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("text3")
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.HasKey("id");

                    b.HasIndex("companyId");

                    b.HasIndex("guestId");

                    b.HasIndex("reviewId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("web.Db.Company", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(22)
                        .HasColumnType("char(22)");

                    b.Property<string>("contactEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("contactPhone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("shortName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("web.Db.Guest", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(22)
                        .HasColumnType("char(22)");

                    b.Property<string>("Reviewid")
                        .HasColumnType("char(22)");

                    b.Property<string>("companyId")
                        .IsRequired()
                        .HasColumnType("char(22)");

                    b.Property<DateTime>("dateBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("varchar(22)");

                    b.Property<string>("gkey")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("varchar(22)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("Reviewid");

                    b.HasIndex("companyId");

                    b.HasIndex("gkey")
                        .IsUnique();

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("web.Db.Review", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(22)
                        .HasColumnType("char(22)");

                    b.Property<bool>("active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("companyId")
                        .IsRequired()
                        .HasColumnType("char(22)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("key")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("varchar(22)");

                    b.Property<string>("question1")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("question2")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("question3")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.HasKey("id");

                    b.HasIndex("companyId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("web.Db.User", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(22)
                        .HasColumnType("char(22)");

                    b.Property<string>("companyId")
                        .IsRequired()
                        .HasColumnType("char(22)");

                    b.Property<string>("contactEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("contactPhone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("dateBirth")
                        .HasMaxLength(255)
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("varchar(22)");

                    b.Property<string>("hash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("salt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("surname")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("companyId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("web.Db.Answer", b =>
                {
                    b.HasOne("web.Db.Company", "Company")
                        .WithMany("answers")
                        .HasForeignKey("companyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("web.Db.Guest", "guest")
                        .WithMany("answers")
                        .HasForeignKey("guestId");

                    b.HasOne("web.Db.Review", "review")
                        .WithMany("answers")
                        .HasForeignKey("reviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("guest");

                    b.Navigation("review");
                });

            modelBuilder.Entity("web.Db.Guest", b =>
                {
                    b.HasOne("web.Db.Review", null)
                        .WithMany("guests")
                        .HasForeignKey("Reviewid");

                    b.HasOne("web.Db.Company", "Company")
                        .WithMany("guests")
                        .HasForeignKey("companyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("web.Db.Review", b =>
                {
                    b.HasOne("web.Db.Company", "Company")
                        .WithMany("reviews")
                        .HasForeignKey("companyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("web.Db.User", b =>
                {
                    b.HasOne("web.Db.Company", "Company")
                        .WithMany("users")
                        .HasForeignKey("companyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("web.Db.Company", b =>
                {
                    b.Navigation("answers");

                    b.Navigation("guests");

                    b.Navigation("reviews");

                    b.Navigation("users");
                });

            modelBuilder.Entity("web.Db.Guest", b =>
                {
                    b.Navigation("answers");
                });

            modelBuilder.Entity("web.Db.Review", b =>
                {
                    b.Navigation("answers");

                    b.Navigation("guests");
                });
#pragma warning restore 612, 618
        }
    }
}
