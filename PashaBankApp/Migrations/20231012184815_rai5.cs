﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PashaBankApp.Migrations
{
    /// <inheritdoc />
    public partial class rai5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AdreessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    AddressInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AdreessID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManagerID = table.Column<int>(type: "int", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    ContactInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contactType = table.Column<int>(type: "int", nullable: false),
                    ContactInformation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.ContactInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    ErrorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorType = table.Column<int>(type: "int", nullable: false),
                    TimeofOccured = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.ErrorId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
                columns: table => new
                {
                    PersonalInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    DocumentSeria = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateofIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuingAuthority = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfos", x => x.PersonalInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpireOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Distributors",
                columns: table => new
                {
                    DistributorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistributorLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ExpireOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recomendedby = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    CountOffInvetedDistributor = table.Column<int>(type: "int", nullable: true),
                    PersonalInfoID = table.Column<int>(type: "int", nullable: false),
                    ContactinfoID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.DistributorID);
                    table.ForeignKey(
                        name: "FK_Distributors_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AdreessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributors_ContactInfos_ContactinfoID",
                        column: x => x.ContactinfoID,
                        principalTable: "ContactInfos",
                        principalColumn: "ContactInfoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributors_PersonalInfos_PersonalInfoID",
                        column: x => x.PersonalInfoID,
                        principalTable: "PersonalInfos",
                        principalColumn: "PersonalInfoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBonus = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DistributorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusID);
                    table.ForeignKey(
                        name: "FK_Bonus_Distributors_DistributorID",
                        column: x => x.DistributorID,
                        principalTable: "Distributors",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributorSales",
                columns: table => new
                {
                    DistributorSaleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpireOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    distributorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorSales", x => x.DistributorSaleID);
                    table.ForeignKey(
                        name: "FK_DistributorSales_Distributors_distributorID",
                        column: x => x.distributorID,
                        principalTable: "Distributors",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributorSales_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_DistributorID",
                table: "Bonus",
                column: "DistributorID");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_AddressID",
                table: "Distributors",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_ContactinfoID",
                table: "Distributors",
                column: "ContactinfoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_PersonalInfoID",
                table: "Distributors",
                column: "PersonalInfoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistributorSales_distributorID",
                table: "DistributorSales",
                column: "distributorID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorSales_ProductID",
                table: "DistributorSales",
                column: "ProductID");
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
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "DistributorSales");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "PersonalInfos");
        }
    }
}
