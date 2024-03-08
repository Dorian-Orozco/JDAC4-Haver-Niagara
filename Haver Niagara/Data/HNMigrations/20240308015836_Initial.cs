using System;
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
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Engineerings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "QualityInspections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InspectorName = table.Column<string>(type: "TEXT", nullable: true),
                    InspectorDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: true),
                    DepartmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemMarked = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReInspected = table.Column<bool>(type: "INTEGER", nullable: false),
                    QualityIdentify = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Name = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "CARs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CARNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
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
                    FollowUpDate = table.Column<DateTime>(type: "TEXT", nullable: false),
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
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PartNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    SAPNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchaseNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    SalesOrder = table.Column<string>(type: "TEXT", nullable: true),
                    ProductNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityRecieved = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDefect = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
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
                    DefectID = table.Column<int>(type: "INTEGER", nullable: false),
                    PartID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectLists", x => x.DefectListID);
                    table.ForeignKey(
                        name: "FK_DefectLists_Defects_DefectID",
                        column: x => x.DefectID,
                        principalTable: "Defects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    NCR_Stage = table.Column<int>(type: "INTEGER", nullable: false),
                    PartID = table.Column<int>(type: "INTEGER", nullable: true),
                    OperationID = table.Column<int>(type: "INTEGER", nullable: true),
                    EngineeringID = table.Column<int>(type: "INTEGER", nullable: true),
                    QualityInspectionID = table.Column<int>(type: "INTEGER", nullable: true)
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
                        name: "FK_NCRs_QualityInspections_QualityInspectionID",
                        column: x => x.QualityInspectionID,
                        principalTable: "QualityInspections",
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
                name: "IX_NCRs_QualityInspectionID",
                table: "NCRs",
                column: "QualityInspectionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SupplierID",
                table: "Parts",
                column: "SupplierID");
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
                name: "QualityInspections");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
