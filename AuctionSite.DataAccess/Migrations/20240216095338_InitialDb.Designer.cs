﻿// <auto-generated />
using System;
using AuctionSite.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuctionSite.DataAccess.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20240216095338_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuctionSite.Core.Models.BetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("CommentsId")
                        .IsUnique();

                    b.HasIndex("LotId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.BuyerEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.CommentsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.LotConcreteEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HaveTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSold")
                        .HasColumnType("bit");

                    b.Property<Guid>("LotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("MaxPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("LotConcretes");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.LotEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePreview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LotConcreteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LotConcreteId")
                        .IsUnique();

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.ReplyCommentsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentsId");

                    b.ToTable("ReplyComments");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.BetEntity", b =>
                {
                    b.HasOne("AuctionSite.Core.Models.BuyerEntity", "Buyer")
                        .WithMany("Bets")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionSite.Core.Models.CommentsEntity", "Comments")
                        .WithOne("Bet")
                        .HasForeignKey("AuctionSite.Core.Models.BetEntity", "CommentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionSite.Core.Models.LotConcreteEntity", "Lot")
                        .WithMany("Bets")
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Comments");

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.BuyerEntity", b =>
                {
                    b.HasOne("AuctionSite.Core.Models.UserEntity", "User")
                        .WithOne("Buyer")
                        .HasForeignKey("AuctionSite.Core.Models.BuyerEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.LotEntity", b =>
                {
                    b.HasOne("AuctionSite.Core.Models.LotConcreteEntity", "LotConcrete")
                        .WithOne("Lot")
                        .HasForeignKey("AuctionSite.Core.Models.LotEntity", "LotConcreteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LotConcrete");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.ReplyCommentsEntity", b =>
                {
                    b.HasOne("AuctionSite.Core.Models.CommentsEntity", "Comments")
                        .WithMany("ReplyComments")
                        .HasForeignKey("CommentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.BuyerEntity", b =>
                {
                    b.Navigation("Bets");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.CommentsEntity", b =>
                {
                    b.Navigation("Bet");

                    b.Navigation("ReplyComments");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.LotConcreteEntity", b =>
                {
                    b.Navigation("Bets");

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("AuctionSite.Core.Models.UserEntity", b =>
                {
                    b.Navigation("Buyer");
                });
#pragma warning restore 612, 618
        }
    }
}
