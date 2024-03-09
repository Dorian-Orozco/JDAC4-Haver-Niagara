using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haver_Niagara.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defect",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defect", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Engineering",
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
                    table.PrimaryKey("PK_Engineering", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
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
                    table.PrimaryKey("PK_Operation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Procurement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReturnRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    RMANumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CarrierName = table.Column<string>(type: "TEXT", nullable: true),
                    CarrierPhone = table.Column<string>(type: "TEXT", nullable: true),
                    AccountNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DisposeOnSite = table.Column<bool>(type: "INTEGER", nullable: false),
                    ToReceiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SuppReturnCompletedSAP = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExpectSuppCredit = table.Column<bool>(type: "INTEGER", nullable: false),
                    BillSupplier = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procurement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QualityInspection",
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
                    table.PrimaryKey("PK_QualityInspection", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAR",
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
                    table.PrimaryKey("PK_CAR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CAR_Operation_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FollowUp",
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
                    table.PrimaryKey("PK_FollowUp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FollowUp_Operation_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Part",
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
                    table.PrimaryKey("PK_Part", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Part_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefectList",
                columns: table => new
                {
                    DefectListID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefectID = table.Column<int>(type: "INTEGER", nullable: false),
                    PartID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefectList", x => x.DefectListID);
                    table.ForeignKey(
                        name: "FK_DefectList_Defect_DefectID",
                        column: x => x.DefectID,
                        principalTable: "Defect",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefectList_Part_PartID",
                        column: x => x.PartID,
                        principalTable: "Part",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
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
                    table.PrimaryKey("PK_Media", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Media_Part_PartID",
                        column: x => x.PartID,
                        principalTable: "Part",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCR",
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
                    PartID = table.Column<int>(type: "INTEGER", nullable: true),
                    OperationID = table.Column<int>(type: "INTEGER", nullable: true),
                    EngineeringID = table.Column<int>(type: "INTEGER", nullable: true),
                    QualityInspectionID = table.Column<int>(type: "INTEGER", nullable: true),
                    ProcurementID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NCR_Engineering_EngineeringID",
                        column: x => x.EngineeringID,
                        principalTable: "Engineering",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCR_Operation_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCR_Part_PartID",
                        column: x => x.PartID,
                        principalTable: "Part",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCR_Procurement_ProcurementID",
                        column: x => x.ProcurementID,
                        principalTable: "Procurement",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NCR_QualityInspection_QualityInspectionID",
                        column: x => x.QualityInspectionID,
                        principalTable: "QualityInspection",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAR_OperationID",
                table: "CAR",
                column: "OperationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DefectList_DefectID",
                table: "DefectList",
                column: "DefectID");

            migrationBuilder.CreateIndex(
                name: "IX_DefectList_PartID",
                table: "DefectList",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUp_OperationID",
                table: "FollowUp",
                column: "OperationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_PartID",
                table: "Media",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_NCR_EngineeringID",
                table: "NCR",
                column: "EngineeringID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCR_OperationID",
                table: "NCR",
                column: "OperationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCR_PartID",
                table: "NCR",
                column: "PartID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCR_ProcurementID",
                table: "NCR",
                column: "ProcurementID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCR_QualityInspectionID",
                table: "NCR",
                column: "QualityInspectionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Part_SupplierID",
                table: "Part",
                column: "SupplierID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CAR");

            migrationBuilder.DropTable(
                name: "DefectList");

            migrationBuilder.DropTable(
                name: "FollowUp");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "NCR");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Defect");

            migrationBuilder.DropTable(
                name: "Engineering");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Part");

            migrationBuilder.DropTable(
                name: "Procurement");

            migrationBuilder.DropTable(
                name: "QualityInspection");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
