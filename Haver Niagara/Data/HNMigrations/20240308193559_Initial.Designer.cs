﻿// <auto-generated />
using System;
using Haver_Niagara.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Haver_Niagara.Data.HNMigrations
{
    [DbContext(typeof(HaverNiagaraDbContext))]
    [Migration("20240308193559_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("PartID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DefectListID");

                    b.HasIndex("DefectID");

                    b.HasIndex("PartID");

                    b.ToTable("DefectLists");
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

                    b.ToTable("Engineerings");
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

                    b.Property<int?>("OperationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("OperationID")
                        .IsUnique();

                    b.ToTable("FollowUps");
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

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Haver_Niagara.Models.NCR", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EngineeringID")
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

                    b.Property<int?>("QualityInspectionID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("EngineeringID")
                        .IsUnique();

                    b.HasIndex("OperationID")
                        .IsUnique();

                    b.HasIndex("PartID")
                        .IsUnique();

                    b.HasIndex("QualityInspectionID")
                        .IsUnique();

                    b.ToTable("NCRs");
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

                    b.ToTable("Operations");
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

                    b.ToTable("Parts");
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

                    b.ToTable("QualityInspections");
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

            modelBuilder.Entity("Haver_Niagara.Models.FileContent", b =>
                {
                    b.HasOne("Haver_Niagara.Models.UploadedFile", "UploadedFile")
                        .WithOne("FileContent")
                        .HasForeignKey("Haver_Niagara.Models.FileContent", "FileContentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadedFile");
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

                    b.HasOne("Haver_Niagara.Models.QualityInspection", "QualityInspection")
                        .WithOne("NCR")
                        .HasForeignKey("Haver_Niagara.Models.NCR", "QualityInspectionID");

                    b.Navigation("Engineering");

                    b.Navigation("Operation");

                    b.Navigation("Part");

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

            modelBuilder.Entity("Haver_Niagara.Models.QualityInspection", b =>
                {
                    b.Navigation("NCR");
                });

            modelBuilder.Entity("Haver_Niagara.Models.Supplier", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("Haver_Niagara.Models.UploadedFile", b =>
                {
                    b.Navigation("FileContent");
                });
#pragma warning restore 612, 618
        }
    }
}
