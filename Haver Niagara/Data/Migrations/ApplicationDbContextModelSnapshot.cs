﻿// <auto-generated />
using System;
using Haver_Niagara.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Haver_Niagara.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.16");

            modelBuilder.Entity("Haver_Niagara.Models.CAR", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CARNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OperationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("OperationID")
                        .IsUnique();

                    b.ToTable("CAR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Defect", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Defect");
                });

            modelBuilder.Entity("Haver_Niagara.Models.DefectList", b =>
                {
                    b.Property<int>("DefectListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefectID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PartID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DefectListID");

                    b.HasIndex("DefectID");

                    b.HasIndex("PartID");

                    b.ToTable("DefectList");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Engineering", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CustomerNotify")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DispositionNotes")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DrawUpdate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EngineeringDisposition")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RevisionDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RevisionOriginal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevisionUpdated")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Engineering");
                });

            modelBuilder.Entity("Haver_Niagara.Models.FollowUp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FollowUpDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FollowUpType")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OperationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("OperationID")
                        .IsUnique();

                    b.ToTable("FollowUp");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Media", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Links")
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("PartID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("PartID");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NCR", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EngineeringID")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsArchived")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("NCR_Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("NCR_Stage")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NCR_Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NewNCRID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OldNCRID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OperationID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PartID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProcurementID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QualityInspectionID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("EngineeringID")
                        .IsUnique();

                    b.HasIndex("OperationID")
                        .IsUnique();

                    b.HasIndex("PartID")
                        .IsUnique();

                    b.HasIndex("ProcurementID")
                        .IsUnique();

                    b.HasIndex("QualityInspectionID")
                        .IsUnique();

                    b.ToTable("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Operation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("OperationCar")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("OperationDecision")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OperationFollowUp")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OperationNotes")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Part", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PartNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PurchaseNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityDefect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityRecieved")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SAPNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SalesOrder")
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Part");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Procurement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BillSupplier")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarrierName")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarrierPhone")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DisposeOnSite")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ExpectSuppCredit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RMANumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReturnRejected")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SuppReturnCompletedSAP")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ToReceiveDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Procurement");
                });

            modelBuilder.Entity("Haver_Niagara.Models.QualityInspection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Department")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartmentDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InspectorDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("InspectorName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ItemMarked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("QualityIdentify")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ReInspected")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("QualityInspection");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Haver_Niagara.Models.CAR", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Operation", "Operation")
                        .WithOne("CAR")
                        .HasForeignKey("Haver_Niagara.Models.CAR", "OperationID");

                    b.Navigation("Operation");
                });

            modelBuilder.Entity("Haver_Niagara.Models.DefectList", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Defect", "Defect")
                        .WithMany("DefectLists")
                        .HasForeignKey("DefectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Haver_Niagara.Models.Part", "Part")
                        .WithMany("DefectLists")
                        .HasForeignKey("PartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Defect");

                    b.Navigation("Part");
                });

            modelBuilder.Entity("Haver_Niagara.Models.FollowUp", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Operation", "Operation")
                        .WithOne("FollowUp")
                        .HasForeignKey("Haver_Niagara.Models.FollowUp", "OperationID");

                    b.Navigation("Operation");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Media", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Part", "Part")
                        .WithMany("Medias")
                        .HasForeignKey("PartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NCR", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Engineering", "Engineering")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "EngineeringID");

                    b.HasOne("Haver_Niagara.Models.Operation", "Operation")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "OperationID");

                    b.HasOne("Haver_Niagara.Models.Part", "Part")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "PartID");

                    b.HasOne("Haver_Niagara.Models.Procurement", "Procurement")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "ProcurementID");

                    b.HasOne("Haver_Niagara.Models.QualityInspection", "QualityInspection")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "QualityInspectionID");

                    b.Navigation("Engineering");

                    b.Navigation("Operation");

                    b.Navigation("Part");

                    b.Navigation("Procurement");

                    b.Navigation("QualityInspection");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Part", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Supplier", "Supplier")
                        .WithMany("Parts")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Haver_Niagara.Models.Defect", b =>
                {
                    b.Navigation("DefectLists");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Engineering", b =>
                {
                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Operation", b =>
                {
                    b.Navigation("CAR");

                    b.Navigation("FollowUp");

                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Part", b =>
                {
                    b.Navigation("DefectLists");

                    b.Navigation("Medias");

                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Procurement", b =>
                {
                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.QualityInspection", b =>
                {
                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Supplier", b =>
                {
                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}