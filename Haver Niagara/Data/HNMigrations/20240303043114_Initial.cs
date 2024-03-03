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
                    RevDate = table.Column<DateTime>(type: "TEXT", nullable: false),
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
                    OperationFollowUp = table.Column<bool>(type: "INTEGER", nullable: false),
                    CarID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.ID);
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
                    PurchaseNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesOrder = table.Column<int>(type: "INTEGER", nullable: false),
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
                    NCR_Number = table.Column<string>(type: "TEXT", nullable: false),
                    NCR_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NCR_Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    NCR_Stage = table.Column<int>(type: "INTEGER", nullable: false),
                    PartID = table.Column<int>(type: "INTEGER", nullable: true),
                    OperationID = table.Column<int>(type: "INTEGER", nullable: true),
                    EngineeringID = table.Column<int>(type: "INTEGER", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "NewNCRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NewNCRNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QualityInspectionID = table.Column<int>(type: "INTEGER", nullable: false),
                    NCRId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewNCRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewNCRs_NCRs_NCRId",
                        column: x => x.NCRId,
                        principalTable: "NCRs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QualityInspection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemMarked = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReInspected = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewNCRID = table.Column<int>(type: "INTEGER", nullable: false),
                    NCRId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityInspection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QualityInspection_NCRs_NCRId",
                        column: x => x.NCRId,
                        principalTable: "NCRs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualityInspection_NewNCRs_NewNCRID",
                        column: x => x.NewNCRID,
                        principalTable: "NewNCRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_NewNCRs_NCRId",
                table: "NewNCRs",
                column: "NCRId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SupplierID",
                table: "Parts",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_QualityInspection_NCRId",
                table: "QualityInspection",
                column: "NCRId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityInspection_NewNCRID",
                table: "QualityInspection",
                column: "NewNCRID",
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
                name: "QualityInspection");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "NewNCRs");

            migrationBuilder.DropTable(
                name: "NCRs");

            migrationBuilder.DropTable(
                name: "Engineerings");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
