﻿// <auto-generated />
using System;
using Haver_Niagara.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Haver_Niagara.Data.HNMigrations
{
    [DbContext(typeof(HaverNiagaraDbContext))]
    partial class HaverNiagaraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("Haver_Niagara.Models.CAR", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CARNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("CARs");
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

                    b.ToTable("Defects");
                });

            modelBuilder.Entity("Haver_Niagara.Models.DefectList", b =>
                {
                    b.Property<int>("DefectListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefectID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DefectListID");

                    b.HasIndex("DefectID");

                    b.HasIndex("ProductID");

                    b.ToTable("DefectLists");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Engineering", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CustomerNotify")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Disposition")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DrawUpdate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EngDecision")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EngSignature")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EngSignatureDate")
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

            modelBuilder.Entity("Haver_Niagara.Models.FileContent", b =>
                {
                    b.Property<int>("FileContentID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.HasKey("FileContentID");

                    b.ToTable("FileContent");
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

                    b.HasKey("ID");

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

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NCR", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EngineeringID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InspectDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("InspectName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NCRClosed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NCR_Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PurchasingID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("QualDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("QualSignature")
                        .HasColumnType("TEXT");

                    b.Property<string>("SalesOrder")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("EngineeringID")
                        .IsUnique();

                    b.HasIndex("ProductID")
                        .IsUnique();

                    b.HasIndex("PurchasingID")
                        .IsUnique();

                    b.ToTable("NCRs");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NewNCR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NCRId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewNCRNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NCRId")
                        .IsUnique();

                    b.ToTable("NewNCRs");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityDefect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityRecieved")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SupplierID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Purchasing", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CARID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PurchaseSignature")
                        .HasColumnType("TEXT");

                    b.Property<int>("PurchasingDec")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SignatureDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("followUpID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CARID");

                    b.HasIndex("followUpID");

                    b.ToTable("Purchasings");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Haver_Niagara.Models.UploadedFile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UploadedFiles");
                });

            modelBuilder.Entity("Haver_Niagara.Models.DefectList", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Defect", "Defect")
                        .WithMany("DefectLists")
                        .HasForeignKey("DefectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Haver_Niagara.Models.Product", "Product")
                        .WithMany("DefectLists")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Defect");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Haver_Niagara.Models.FileContent", b =>
                {
                    b.HasOne("Haver_Niagara.Models.UploadedFile", "UploadedFile")
                        .WithOne("FileContent")
                        .HasForeignKey("Haver_Niagara.Models.FileContent", "FileContentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Media", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Product", "Product")
                        .WithMany("Medias")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NCR", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Engineering", "Engineering")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "EngineeringID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Haver_Niagara.Models.Product", "Product")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Haver_Niagara.Models.Purchasing", "Purchasing")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "PurchasingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineering");

                    b.Navigation("Product");

                    b.Navigation("Purchasing");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NewNCR", b =>
                {
                    b.HasOne("Haver_Niagara.Models.NCR", "NCR")
                        .WithOne("NewNCR")
                        .HasForeignKey("Haver_Niagara.Models.NewNCR", "NCRId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Product", b =>
                {
                    b.HasOne("Haver_Niagara.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Purchasing", b =>
                {
                    b.HasOne("Haver_Niagara.Models.CAR", "CAR")
                        .WithMany()
                        .HasForeignKey("CARID");

                    b.HasOne("Haver_Niagara.Models.FollowUp", "followUp")
                        .WithMany()
                        .HasForeignKey("followUpID");

                    b.Navigation("CAR");

                    b.Navigation("followUp");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Defect", b =>
                {
                    b.Navigation("DefectLists");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Engineering", b =>
                {
                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NCR", b =>
                {
                    b.Navigation("NewNCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Product", b =>
                {
                    b.Navigation("DefectLists");

                    b.Navigation("Medias");

                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Purchasing", b =>
                {
                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Haver_Niagara.Models.UploadedFile", b =>
                {
                    b.Navigation("FileContent");
                });
#pragma warning restore 612, 618
        }
    }
}
