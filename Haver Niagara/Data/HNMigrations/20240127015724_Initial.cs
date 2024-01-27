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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CARNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FollowUp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FollowUpType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUp", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Purchasing",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignatureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasingDec = table.Column<int>(type: "int", nullable: false),
                    followUpID = table.Column<int>(type: "int", nullable: true),
                    CARID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchasing", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchasing_CARs_CARID",
                        column: x => x.CARID,
                        principalTable: "CARs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Purchasing_FollowUp_followUpID",
                        column: x => x.followUpID,
                        principalTable: "FollowUp",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityRecieved = table.Column<int>(type: "int", nullable: false),
                    QuantityDefect = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DefectList",
                columns: table => new
                {
                    DefectListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectList", x => x.DefectListID);
                    table.ForeignKey(
                        name: "FK_DefectList_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NCR_Number = table.Column<int>(type: "int", nullable: false),
                    SalesOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NCRClosed = table.Column<bool>(type: "bit", nullable: false),
                    QualSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QualDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    PurchasingID = table.Column<int>(type: "int", nullable: false),
                    NewNCRId = table.Column<int>(type: "int", nullable: false),
                    EngineerId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_NCRs_Purchasing_PurchasingID",
                        column: x => x.PurchasingID,
                        principalTable: "Purchasing",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectDefectList",
                columns: table => new
                {
                    DefectListsDefectListID = table.Column<int>(type: "int", nullable: false),
                    DefectsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectDefectList", x => new { x.DefectListsDefectListID, x.DefectsID });
                    table.ForeignKey(
                        name: "FK_DefectDefectList_DefectList_DefectListsDefectListID",
                        column: x => x.DefectListsDefectListID,
                        principalTable: "DefectList",
                        principalColumn: "DefectListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefectDefectList_Defects_DefectsID",
                        column: x => x.DefectsID,
                        principalTable: "Defects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engineering",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerNotify = table.Column<bool>(type: "bit", nullable: false),
                    DrawUpdate = table.Column<bool>(type: "bit", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevisionOriginal = table.Column<int>(type: "int", nullable: false),
                    RevisionUpdated = table.Column<int>(type: "int", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EngSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngSignatureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EngDecision = table.Column<int>(type: "int", nullable: false),
                    NCRId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewNCRNumber = table.Column<int>(type: "int", nullable: false),
                    NCRId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DefectDefectList_DefectsID",
                table: "DefectDefectList",
                column: "DefectsID");

            migrationBuilder.CreateIndex(
                name: "IX_DefectList_ProductID",
                table: "DefectList",
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
                name: "IX_Purchasing_CARID",
                table: "Purchasing",
                column: "CARID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchasing_followUpID",
                table: "Purchasing",
                column: "followUpID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefectDefectList");

            migrationBuilder.DropTable(
                name: "Engineering");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "NewNCRs");

            migrationBuilder.DropTable(
                name: "DefectList");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "NCRs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchasing");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "CARs");

            migrationBuilder.DropTable(
                name: "FollowUp");
        }
    }
}
