using Haver_Niagara.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace Haver_Niagara.Data
{
    public static class HaverInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            HaverNiagaraDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<HaverNiagaraDbContext>();


           // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (!context.CARs.Any())
            {
                var cars = new List<CAR>
                    {
                        new CAR {ID = 1, CARNumber = 12345},
                        new CAR {ID = 2, CARNumber = 98765},
                        new CAR {ID = 3, CARNumber = 56748},
                        new CAR {ID = 4, CARNumber = 84379},
                        new CAR {ID = 5, CARNumber = 93842},
                        new CAR {ID = 6, CARNumber = 42981},
                        new CAR {ID = 7, CARNumber = 29013},
                        new CAR {ID = 8, CARNumber = 90842},
                        new CAR {ID = 9, CARNumber = 32491},
                        new CAR {ID = 10, CARNumber = 31904}
                    };
                context.CARs.AddRange(cars);
                context.SaveChanges();
            }
            if (!context.Defects.Any())
            {
                var defects = new List<Defect>
                    {
                        new Defect {ID = 1, Name = "Design Error (Drawing)", Description = "Incorrect design drawing"},
                        new Defect {ID = 2, Name = "Holes not tapped", Description = "Holes not tapped"},
                        new Defect {ID = 3, Name = "Incorrect dimensions", Description = "Dimensions are incorrect"},
                        new Defect {ID = 4, Name = "Incorrect hardware", Description = "Wrong hardware"},
                        new Defect {ID = 5, Name = "Incorrect hole (size,locations,missing)", Description = "Hole is wrong size, wrong location or missing"},
                        new Defect {ID = 6, Name = "Incorrect thread size", Description = "Thread size is incorrect"},
                        new Defect {ID = 7, Name = "Not Painted", Description = "Item not painted"},
                        new Defect {ID = 8, Name = "Poor Paint finish", Description = "Item painted poorly"},
                        new Defect {ID = 9, Name = "Poor quality surface finish", Description = "Item surface finished poorly"},
                        new Defect {ID = 10, Name = "Poor Weld quality", Description = "Item welded poorly"},
                        new Defect {ID = 11, Name = "Incorrect fit", Description = "Item doesn't fit"},
                        new Defect {ID = 12, Name = "Incorrect weld size", Description = "Weld size incorrect"},
                        new Defect {ID = 13, Name = "Missing Items", Description = "Missing items"},
                        new Defect {ID = 14, Name = "Broken / Twisted Wires", Description = "Wires damaged"},
                        new Defect {ID = 15, Name = "Out of Crimp", Description = "Out of crimp"},
                        new Defect {ID = 16, Name = "Incorrect Specification", Description = "Specification incorrect"},
                        new Defect {ID = 17, Name = "Incorrect Hook / Hook Orientation", Description = "Wrong hook or wrong hook orieantation"},
                        new Defect {ID = 18, Name = "Incorrect Center Hole Punching", Description = "Center hole punched incorrect"},
                        new Defect {ID = 19, Name = "Missing Center Hole Punching", Description = "Center hole punch missing"},
                        new Defect {ID = 20, Name = "Incorrect hardware installation", Description = "Hardware installed incorrectly"},
                        new Defect {ID = 21, Name = "Incorrect labelling", Description = "Label is incorrect"},
                        new Defect {ID = 22, Name = "Drawing not updated", Description = "Drawing is now updated"},
                        new Defect {ID = 23, Name = "Incorrect material", Description = "Material is incorrect"},
                        new Defect {ID = 24, Name = "Delivery Quality", Description = "Delivered in poor quality"},
                        new Defect {ID = 25, Name = "Finishing error (M.W. STC)", Description = "Error with the finishing"},
                        new Defect {ID = 26, Name = "Incorrect component (FMP package)", Description = "Wrong component"},
                    };
                context.Defects.AddRange(defects);
                context.SaveChanges();
            }

            if (!context.Suppliers.Any())
            {
                context.Suppliers.AddRange(
                new Supplier
                {
                    ID = 1,
                    Name = "HINGSTON METAL FABRICATORS",
                },
                new Supplier
                {
                    ID = 2,
                    Name = "W S TYLER - PARTICLE & FINE",
                },
                new Supplier
                {
                    ID = 3,
                    Name = "VALLEY RUBBER, LLC",
                },
                new Supplier
                {
                    ID = 4,
                    Name = "RIGHT MACHINE INDUSTRIAL",
                },
                new Supplier
                {
                    ID = 5,
                    Name = "KAVON MACHINE INC",
                },
                new Supplier
                {
                    ID = 6,
                    Name = "NIAGARA PRECISION LTD",
                },
                new Supplier
                {
                    ID = 7,
                    Name = "BICKLE MAIN INDUSTRIAL SUPPLY INC.",
                });
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    ID = 1,
                    Name = "Wheels",
                    ProductNumber = 208475893,
                    QuantityRecieved = 20,
                    QuantityDefect = 20,
                    Description = "Replacement wheels",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 1)
                },
                new Product
                {
                    ID = 2,
                    Name = "Wires",
                    ProductNumber = 206547333,
                    QuantityRecieved = 10,
                    QuantityDefect = 10,
                    Description = "Replacement wires",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 2)
                },
                new Product
                {
                    ID = 3,
                    Name = "Steel Panels",
                    ProductNumber = 207843292,
                    QuantityRecieved = 8,
                    QuantityDefect = 2,
                    Description = "Steel panels for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 2)
                },
                new Product
                {
                    ID = 4,
                    Name = "Bolts",
                    ProductNumber = 205231782,
                    QuantityRecieved = 100,
                    QuantityDefect = 100,
                    Description = "Bolts for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 3)
                },
                new Product
                {
                    ID = 5,
                    Name = "Nuts",
                    ProductNumber = 209031293,
                    QuantityRecieved = 200,
                    QuantityDefect = 100,
                    Description = "Nuts for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 4)
                },
                new Product
                {
                    ID = 6,
                    Name = "Screws",
                    ProductNumber = 209031296,
                    QuantityRecieved = 200,
                    QuantityDefect = 10,
                    Description = "Screws for repairs.",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 4)
                },
                new Product
                {
                    ID = 7,
                    Name = "Rivets",
                    ProductNumber = 209031299,
                    QuantityRecieved = 100,
                    QuantityDefect = 40,
                    Description = "Rivets for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 5)
                },
                new Product
                {
                    ID = 8,
                    Name = "Washers",
                    ProductNumber = 244110399,
                    QuantityRecieved = 50,
                    QuantityDefect = 1,
                    Description = "Washers for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 5)
                },
                new Product
                      {
                    ID = 9,
                    Name = "Anchors",
                    ProductNumber = 204231293,
                    QuantityRecieved = 10,
                    QuantityDefect = 1,
                    Description = "Anchors for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 6)
                },
                new Product
                {
                    ID = 10,
                    Name = "Nails",
                    ProductNumber = 212031293,
                    QuantityRecieved = 200,
                    QuantityDefect = 54,
                    Description = "Nails for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 7)
                },
                new Product
                {
                    ID = 11,
                    Name = "Clips",
                    ProductNumber = 219031293,
                    QuantityRecieved = 200,
                    QuantityDefect = 29,
                    Description = "Clips for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 7)
                });
                context.SaveChanges();
            }
            if (!context.Purchasings.Any())
            {
                //REPRESENTS A FORM NOT A PERSON! THERE HAS TO BE ONE To ONE WITH NCR. 10 NCRS = 10 PURCHASING 
                context.Purchasings.AddRange(
                new Purchasing
                {
                    ID = 1,
                    PurchaseSignature = "James Jones",
                    SignatureDate = DateTime.Parse("2024-01-20"),
                    PurchasingDec = 0
                },
                new Purchasing
                {
                    ID = 2,
                    PurchaseSignature = "Jane Little",
                    SignatureDate = DateTime.Parse("2024-01-22"),
                    PurchasingDec = (PurchasingDecision)1
                },
                new Purchasing
                {
                    ID = 3,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 4,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 5,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 6,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 7,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 8,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 9,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 10,
                    PurchaseSignature = "Matt Turner",
                    SignatureDate = DateTime.Parse("2024-01-24"),
                    PurchasingDec = (PurchasingDecision)2
                },
                new Purchasing
                {
                    ID = 11,
                    PurchaseSignature = "James Jones",
                    SignatureDate = DateTime.Parse("2024-01-20"),
                    PurchasingDec = (PurchasingDecision)4
                });
                
                context.SaveChanges();
            }
            if (!context.NCRs.Any())
            {
                context.NCRs.AddRange(
                new NCR
                {
                    ID = 1,
                    NCR_Number = 1,
                    SalesOrder = "Stock",
                    InspectName = "Kevin Butler",
                    InspectDate = DateTime.Parse("2024-01-10"),
                    NCRClosed = false,
                    QualSignature = "Tom Warner",
                    QualDate = DateTime.Parse("2024-01-11"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 1),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 1),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 1)
                },
                new NCR
                {
                    ID = 2,
                    NCR_Number = 2,
                    SalesOrder = "Stock",
                    InspectName = "Paul Miller",
                    InspectDate = DateTime.Parse("2024-01-11"),
                    NCRClosed = true,
                    QualSignature = "Frank Curry",
                    QualDate = DateTime.Parse("2024-01-12"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 2),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 2),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 2)
                },
                new NCR
                {
                    ID = 3,
                    NCR_Number = 3,
                    SalesOrder = "Stock",
                    InspectName = "Larry Thomson",
                    InspectDate = DateTime.Parse("2024-01-12"),
                    NCRClosed = false,
                    QualSignature = "Neil Horton",
                    QualDate = DateTime.Parse("2024-01-13"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 3),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 3),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 3)
                },
                new NCR
                {
                    ID = 4,
                    NCR_Number = 4,
                    SalesOrder = "Stock",
                    InspectName = "Kevin Butler",
                    InspectDate = DateTime.Parse("2024-01-15"),
                    NCRClosed = false,
                    QualSignature = "Neil Horton",
                    QualDate = DateTime.Parse("2024-01-15"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 4),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 4),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 4)
                },
                new NCR
                {
                    ID = 5,
                    NCR_Number = 5,
                    SalesOrder = "Stock",
                    InspectName = "Larry Thomson",
                    InspectDate = DateTime.Parse("2024-01-11"),
                    NCRClosed = false,
                    QualSignature = "Frank Curry",
                    QualDate = DateTime.Parse("2024-01-13"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 5),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 5),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 5)
                },
                new NCR
                {
                    ID = 6,
                    NCR_Number = 6,
                    SalesOrder = "Stock",
                    InspectName = "Paul Miller",
                    InspectDate = DateTime.Parse("2024-01-18"),
                    NCRClosed = false,
                    QualSignature = "Tom Warner",
                    QualDate = DateTime.Parse("2024-01-19"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 6),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 6),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 6)
                },
                new NCR
                {
                    ID = 7,
                    NCR_Number = 7,
                    SalesOrder = "Stock",
                    InspectName = "Kevin Butler",
                    InspectDate = DateTime.Parse("2024-01-16"),
                    NCRClosed = true,
                    QualSignature = "Frank Curry",
                    QualDate = DateTime.Parse("2024-01-16"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 7),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 7),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 7)
                },
                new NCR
                {
                    ID = 8,
                    NCR_Number = 8,
                    SalesOrder = "Stock",
                    InspectName = "Kevin Butler",
                    InspectDate = DateTime.Parse("2024-01-13"),
                    NCRClosed = false,
                    QualSignature = "Tom Warner",
                    QualDate = DateTime.Parse("2024-01-15"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 8),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 8),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 8)
                },
                new NCR
                {
                    ID = 9,
                    NCR_Number = 9,
                    SalesOrder = "Stock",
                    InspectName = "Larry Thomson",
                    InspectDate = DateTime.Parse("2024-01-14"),
                    NCRClosed = false,
                    QualSignature = "Tom Warner",
                    QualDate = DateTime.Parse("2024-01-17"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 9),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 9),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 9)
                },
                new NCR
                {
                    ID = 10,
                    NCR_Number = 10,
                    SalesOrder = "Stock",
                    InspectName = "Kevin Butler",
                    InspectDate = DateTime.Parse("2024-01-20"),
                    NCRClosed = false,
                    QualSignature = "Neil Horton",
                    QualDate = DateTime.Parse("2024-01-20"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 10),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 10),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 10)
                },
                new NCR
                {
                    ID = 11,
                    NCR_Number = 11,
                    SalesOrder = "Stock",
                    InspectName = "Paul Miller",
                    InspectDate = DateTime.Parse("2024-01-09"),
                    NCRClosed = false,
                    QualSignature = "Frank Curry",
                    QualDate = DateTime.Parse("2024-01-11"),
                    Product = context.Products.FirstOrDefault(p => p.ID == 11),
                    Engineering = context.Engineering.FirstOrDefault(p => p.ID == 11),
                    Purchasing = context.Purchasings.FirstOrDefault(p => p.ID == 11)
                });
                context.SaveChanges();
            }
            if (!context.Engineering.Any())
            {
                context.Engineering.AddRange(
                new Engineering
                {
                    ID = 1,
                    CustomerNotify = false,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 12345,
                    RevisionUpdated = 12346,
                    RevisionDate = DateTime.Parse("2024-01-20"),
                    EngSignature = "John Smith",
                    EngSignatureDate = DateTime.Parse("2024-01-20"),
                    EngDecision = 0,
                    NCRId = 1,

                },
                new Engineering
                {
                    ID = 2,
                    CustomerNotify = true,
                    DrawUpdate = true,
                    Disposition = "N/A",
                    RevisionOriginal = 32434,
                    RevisionUpdated = 52123,
                    RevisionDate = DateTime.Parse("2024-01-22"),
                    EngSignature = "James Brady",
                    EngSignatureDate = DateTime.Parse("2024-01-22"),
                    EngDecision = (EngineeringDecision)1,
                    NCRId = 2,

                },
                new Engineering
                {
                    ID = 3,
                    CustomerNotify = false,
                    DrawUpdate = true,
                    Disposition = "N/A",
                    RevisionOriginal = 87645,
                    RevisionUpdated = 54632,
                    RevisionDate = DateTime.Parse("2024-01-24"),
                    EngSignature = "Linda Johnson",
                    EngSignatureDate = DateTime.Parse("2024-01-24"),
                    EngDecision = (EngineeringDecision)2,
                    NCRId = 3,

                },
                new Engineering
                {
                    ID = 4,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 4,

                },
                new Engineering
                {
                    ID = 5,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 5,

                },
                new Engineering
                {
                    ID = 6,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 6,
                },
                new Engineering
                {
                    ID = 7,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 7,
                },
                new Engineering
                {
                    ID = 8,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 8,
                },
                new Engineering
                {
                    ID = 9,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 9,
                },
                new Engineering
                {
                    ID = 10,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 10,
                },
                new Engineering
                {
                    ID = 11,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    Disposition = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    EngSignature = "Luke Miller",
                    EngSignatureDate = DateTime.Parse("2024-01-26"),
                    EngDecision = (EngineeringDecision)3,
                    NCRId = 11,

                });
                context.SaveChanges();
            }
            if (!context.FollowUp.Any())
            {
                context.FollowUp.AddRange(
                new FollowUp
                {
                    ID = 1,
                    FollowUpDate = DateTime.Parse("2024-01-29"),
                    FollowUpType = "Contact New Supplier to Fufill Order"
                },
                new FollowUp
                {
                    ID = 2,
                    FollowUpDate = DateTime.Parse("2024-01-30"),
                    FollowUpType = "Contact Supplier to Re-Order to Specifications"
                },
                new FollowUp
                {
                    ID = 3,
                    FollowUpDate = DateTime.Parse("2024-01-31"),
                    FollowUpType = "Contact Customer to Confirm Acceptable"
                });
                context.SaveChanges();
            }
            if (!context.DefectLists.Any())
            {
                var defectLists = new List<DefectList>
                {
                    new DefectList { DefectID = 1, ProductID = 1},
                    new DefectList { DefectID = 2, ProductID = 2},
                    new DefectList { DefectID = 3, ProductID = 3},
                    new DefectList { DefectID = 4, ProductID = 4},
                    new DefectList { DefectID = 5, ProductID = 5},
                    new DefectList { DefectID = 6, ProductID = 6},
                    new DefectList { DefectID = 7, ProductID = 7},
                    new DefectList { DefectID = 8, ProductID = 8},
                    new DefectList { DefectID = 9, ProductID = 9},
                    new DefectList { DefectID = 10, ProductID = 10},
                    new DefectList { DefectID = 11, ProductID = 11},
                };
                context.DefectLists.AddRange(defectLists);
                context.SaveChanges();
            }
            
        }
    }
}