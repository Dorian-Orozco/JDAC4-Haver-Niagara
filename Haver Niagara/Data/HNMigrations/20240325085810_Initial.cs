﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haver_Niagara.Data.HNMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Engineerings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerNotify = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrawUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    DispositionNotes = table.Column<string>(type: "TEXT", nullable: true),
                    RevisionOriginal = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionUpdated = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EngineeringDisposition = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineerings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    OperationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OperationDecision = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationNotes = table.Column<string>(type: "TEXT", nullable: true),
                    OperationCar = table.Column<bool>(type: "INTEGER", nullable: false),
                    OperationFollowUp = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Procurements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReturnRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    RMANumber = table.Column<int>(type: "INTEGER", nullable: true),
                    CarrierName = table.Column<string>(type: "TEXT", nullable: true),
                    CarrierPhone = table.Column<string>(type: "TEXT", nullable: true),
                    AccountNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    DisposeOnSite = table.Column<bool>(type: "INTEGER", nullable: false),
                    ToReceiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SuppReturnCompletedSAP = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpectSuppCredit = table.Column<bool>(type: "INTEGER", nullable: false),
                    BillSupplier = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procurements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QualityInspectionFinals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReInspected = table.Column<bool>(type: "INTEGER", nullable: false),
                    InspectorName = table.Column<string>(type: "TEXT", nullable: true),
                    InspectorDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: true),
                    DepartmentDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityInspectionFinals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QualityInspections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QualityIdentify = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemMarked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityInspections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PushEndpoint = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    PushP256DH = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    PushAuth = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CARs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CARNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OperationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CARs_Operations_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FollowUps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FollowUpDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FollowUpType = table.Column<string>(type: "TEXT", nullable: true),
                    OperationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FollowUps_Operations_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PartNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    SAPNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchaseNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    SalesOrder = table.Column<string>(type: "TEXT", nullable: true),
                    ProductNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityRecieved = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDefect = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parts_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    FileContentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.FileContentID);
                    table.ForeignKey(
                        name: "FK_FileContent_UploadedFiles_FileContentID",
                        column: x => x.FileContentID,
                        principalTable: "UploadedFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectLists",
                columns: table => new
                {
                    DefectListID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefectID = table.Column<int>(type: "INTEGER", nullable: true),
                    PartID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectLists", x => x.DefectListID);
                    table.ForeignKey(
                        name: "FK_DefectLists_Defects_DefectID",
                        column: x => x.DefectID,
                        principalTable: "Defects",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DefectLists_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Links = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    PartID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medias_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCRs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NCR_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NCR_Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewNCRID = table.Column<int>(type: "INTEGER", nullable: true),
                    OldNCRID = table.Column<int>(type: "INTEGER", nullable: true),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: true),
                    NCR_Stage = table.Column<int>(type: "INTEGER", nullable: false),
                    NCRSupplierID = table.Column<int>(type: "INTEGER", nullable: true),
                    PartID = table.Column<int>(type: "INTEGER", nullable: true),
                    OperationID = table.Column<int>(type: "INTEGER", nullable: true),
                    EngineeringID = table.Column<int>(type: "INTEGER", nullable: true),
                    QualityInspectionID = table.Column<int>(type: "INTEGER", nullable: true),
                    ProcurementID = table.Column<int>(type: "INTEGER", nullable: true),
                    QualityInspectionFinalID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCRs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NCRs_Engineerings_EngineeringID",
                        column: x => x.EngineeringID,
                        principalTable: "Engineerings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCRs_Operations_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCRs_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCRs_Procurements_ProcurementID",
                        column: x => x.ProcurementID,
                        principalTable: "Procurements",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCRs_QualityInspectionFinals_QualityInspectionFinalID",
                        column: x => x.QualityInspectionFinalID,
                        principalTable: "QualityInspectionFinals",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCRs_QualityInspections_QualityInspectionID",
                        column: x => x.QualityInspectionID,
                        principalTable: "QualityInspections",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCRs_Suppliers_NCRSupplierID",
                        column: x => x.NCRSupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARs_OperationID",
                table: "CARs",
                column: "OperationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DefectLists_DefectID",
                table: "DefectLists",
                column: "DefectID");

            migrationBuilder.CreateIndex(
                name: "IX_DefectLists_PartID",
                table: "DefectLists",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_Name",
                table: "Defects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Phone",
                table: "Employees",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowUps_OperationID",
                table: "FollowUps",
                column: "OperationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_PartID",
                table: "Medias",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_EngineeringID",
                table: "NCRs",
                column: "EngineeringID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_NCRSupplierID",
                table: "NCRs",
                column: "NCRSupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_OperationID",
                table: "NCRs",
                column: "OperationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_PartID",
                table: "NCRs",
                column: "PartID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_ProcurementID",
                table: "NCRs",
                column: "ProcurementID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_QualityInspectionFinalID",
                table: "NCRs",
                column: "QualityInspectionFinalID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_QualityInspectionID",
                table: "NCRs",
                column: "QualityInspectionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SupplierID",
                table: "Parts",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_EmployeeID",
                table: "Subscriptions",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                table: "Suppliers",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARs");

            migrationBuilder.DropTable(
                name: "DefectLists");

            migrationBuilder.DropTable(
                name: "FileContent");

            migrationBuilder.DropTable(
                name: "FollowUps");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "NCRs");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "Engineerings");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Procurements");

            migrationBuilder.DropTable(
                name: "QualityInspectionFinals");

            migrationBuilder.DropTable(
                name: "QualityInspections");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}