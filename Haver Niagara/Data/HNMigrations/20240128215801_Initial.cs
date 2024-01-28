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
                name: "CARs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CARNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FollowUp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FollowUpDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FollowUpType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUp", x => x.ID);
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
                name: "Purchasings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseSignature = table.Column<string>(type: "TEXT", nullable: false),
                    SignatureDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PurchasingDec = table.Column<int>(type: "INTEGER", nullable: false),
                    followUpID = table.Column<int>(type: "INTEGER", nullable: true),
                    CARID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchasings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchasings_CARs_CARID",
                        column: x => x.CARID,
                        principalTable: "CARs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Purchasings_FollowUp_followUpID",
                        column: x => x.followUpID,
                        principalTable: "FollowUp",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ProductNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityRecieved = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDefect = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
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
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
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
                        name: "FK_DefectLists_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCRs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NCR_Number = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesOrder = table.Column<string>(type: "TEXT", nullable: false),
                    InspectName = table.Column<string>(type: "TEXT", nullable: false),
                    InspectDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NCRClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    QualSignature = table.Column<string>(type: "TEXT", nullable: false),
                    QualDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchasingID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCRs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NCRs_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NCRs_Purchasings_PurchasingID",
                        column: x => x.PurchasingID,
                        principalTable: "Purchasings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engineering",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerNotify = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrawUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disposition = table.Column<string>(type: "TEXT", nullable: false),
                    RevisionOriginal = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionUpdated = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EngSignature = table.Column<string>(type: "TEXT", nullable: false),
                    EngSignatureDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EngDecision = table.Column<int>(type: "INTEGER", nullable: false),
                    NCRId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineering", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Engineering_NCRs_NCRId",
                        column: x => x.NCRId,
                        principalTable: "NCRs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewNCRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NewNCRNumber = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "FileContent",
                columns: table => new
                {
                    FileContentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DefectLists_DefectID",
                table: "DefectLists",
                column: "DefectID");

            migrationBuilder.CreateIndex(
                name: "IX_DefectLists_ProductID",
                table: "DefectLists",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Engineering_NCRId",
                table: "Engineering",
                column: "NCRId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_ProductID",
                table: "Media",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_ProductID",
                table: "NCRs",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_NCRs_PurchasingID",
                table: "NCRs",
                column: "PurchasingID");

            migrationBuilder.CreateIndex(
                name: "IX_NewNCRs_NCRId",
                table: "NewNCRs",
                column: "NCRId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchasings_CARID",
                table: "Purchasings",
                column: "CARID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchasings_followUpID",
                table: "Purchasings",
                column: "followUpID");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_ProductID",
                table: "UploadedFiles",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefectLists");

            migrationBuilder.DropTable(
                name: "Engineering");

            migrationBuilder.DropTable(
                name: "FileContent");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "NewNCRs");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "NCRs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchasings");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "CARs");

            migrationBuilder.DropTable(
                name: "FollowUp");
        }
    }
}
