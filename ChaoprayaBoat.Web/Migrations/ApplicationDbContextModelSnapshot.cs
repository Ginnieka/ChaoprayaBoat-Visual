﻿// <auto-generated />
using ChaoprayaBoat.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ChaoprayaBoat.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Boat", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Detail");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Boats");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.BoatHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArriveDateTime");

                    b.Property<int>("CoordinateId");

                    b.Property<int>("TimeTableId");

                    b.HasKey("Id");

                    b.HasIndex("CoordinateId");

                    b.HasIndex("TimeTableId");

                    b.ToTable("BoatHistories");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Coordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodePort");

                    b.Property<int>("CoordinateTypeId");

                    b.Property<string>("Description");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longtitude");

                    b.Property<string>("Name");

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.HasIndex("CoordinateTypeId");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.CoordinateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CoordinateTypes");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("PortCount");

                    b.Property<int>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Emergency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.ToTable("Emergencies");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.InfoPort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("InfoPorts");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .HasMaxLength(45);

                    b.Property<string>("FacebookId")
                        .HasMaxLength(50);

                    b.Property<string>("FacebookPicture")
                        .HasMaxLength(256);

                    b.Property<string>("FacebookToken");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<int>("MemberTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("Password")
                        .HasMaxLength(45);

                    b.Property<string>("Position")
                        .HasMaxLength(45);

                    b.Property<string>("Status")
                        .HasMaxLength(45);

                    b.HasKey("Id");

                    b.HasIndex("MemberTypeId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.MemberHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datetime");

                    b.Property<double>("DestinationCoordinteId");

                    b.Property<int>("MemberId");

                    b.Property<double>("SourceCoordinateId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberHistories");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.MemberType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MemberTypes");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorCode");

                    b.Property<string>("FlagColor");

                    b.Property<string>("PriceDesc");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.RouteCoordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CoordinateId");

                    b.Property<int>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("CoordinateId");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteCoordinates");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.TimeTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BoatId");

                    b.Property<string>("Cashier");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Destination");

                    b.Property<bool>("IsActive");

                    b.Property<int>("MemberId");

                    b.Property<int>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("BoatId");

                    b.HasIndex("MemberId");

                    b.HasIndex("RouteId");

                    b.ToTable("TimeTables");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Travel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CoordinateId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longtitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CoordinateId");

                    b.ToTable("Travels");
                });

            modelBuilder.Entity("ChaoprayaBoat.Web.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.BoatHistory", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.Coordinate", "Coordinate")
                        .WithMany("BoatHistories")
                        .HasForeignKey("CoordinateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChaoprayaBoat.Library.Models.TimeTable", "TimeTable")
                        .WithMany("BoatHistories")
                        .HasForeignKey("TimeTableId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Coordinate", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.CoordinateType", "CoordinateType")
                        .WithMany("Coordinates")
                        .HasForeignKey("CoordinateTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Cost", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.Route", "Route")
                        .WithMany("Costs")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Member", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.MemberType", "MemberType")
                        .WithMany("Members")
                        .HasForeignKey("MemberTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.MemberHistory", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.Member")
                        .WithMany("MemberHistories")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.RouteCoordinate", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.Coordinate", "Coordinate")
                        .WithMany("RouteCoordinates")
                        .HasForeignKey("CoordinateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChaoprayaBoat.Library.Models.Route", "Route")
                        .WithMany("RouteCoordinates")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.TimeTable", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.Boat", "Boat")
                        .WithMany("TimeTables")
                        .HasForeignKey("BoatId");

                    b.HasOne("ChaoprayaBoat.Library.Models.Member", "Member")
                        .WithMany("TimeTables")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChaoprayaBoat.Library.Models.Route", "Route")
                        .WithMany("TimeTables")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChaoprayaBoat.Library.Models.Travel", b =>
                {
                    b.HasOne("ChaoprayaBoat.Library.Models.Coordinate", "Coordinate")
                        .WithMany("Travels")
                        .HasForeignKey("CoordinateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ChaoprayaBoat.Web.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ChaoprayaBoat.Web.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChaoprayaBoat.Web.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ChaoprayaBoat.Web.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
