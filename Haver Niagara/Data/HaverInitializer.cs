using Haver_Niagara.Models;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Diagnostics;
using System.Text;
namespace Haver_Niagara.Data
{
    public static class HaverInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            HaverNiagaraDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<HaverNiagaraDbContext>();


            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (!context.CARs.Any())
            {
                var cars = new List<CAR>
                    {
                        new CAR {ID = 1, Date = DateTime.Parse("2024-01-22"), CARNumber = 12345 },
                        new CAR {ID = 2, Date = DateTime.Parse("2024-01-22"), CARNumber = 98765 },
                        new CAR {ID = 3, Date = DateTime.Parse("2024-01-22"), CARNumber = 56748 },
                        new CAR {ID = 4, Date = DateTime.Parse("2024-01-22"), CARNumber = 84379 },
                        new CAR {ID = 5, Date = DateTime.Parse("2024-01-22"), CARNumber = 93842 },
                        new CAR {ID = 6, Date = DateTime.Parse("2024-01-22"), CARNumber = 42981 },
                        new CAR {ID = 7, Date = DateTime.Parse("2024-01-22"), CARNumber = 29013 },
                        new CAR {ID = 8, Date = DateTime.Parse("2024-01-22"), CARNumber = 90842},
                        new CAR {ID = 9, Date = DateTime.Parse("2024-01-22"), CARNumber = 32491},
                        new CAR {ID = 10, Date = DateTime.Parse("2024-01-22"), CARNumber = 31904}
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
            if (!context.Parts.Any())
            {
                context.Parts.AddRange(
                new Part
                {
                    ID = 1,
                    Name = "Wheels",
                    ProductNumber = 208475893,
                    QuantityRecieved = 20,
                    QuantityDefect = 20,
                    Description = "Replacement wheels",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 1),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock",
                },
                new Part
                {
                    ID = 2,
                    Name = "Wires",
                    ProductNumber = 206547333,
                    QuantityRecieved = 10,
                    QuantityDefect = 10,
                    Description = "Replacement wires",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 2),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 3,
                    Name = "Steel Panels",
                    ProductNumber = 207843292,
                    QuantityRecieved = 8,
                    QuantityDefect = 2,
                    Description = "Steel panels for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 2),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 4,
                    Name = "Bolts",
                    ProductNumber = 205231782,
                    QuantityRecieved = 100,
                    QuantityDefect = 100,
                    Description = "Bolts for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 3),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 5,
                    Name = "Nuts",
                    ProductNumber = 209031293,
                    QuantityRecieved = 200,
                    QuantityDefect = 100,
                    Description = "Nuts for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 4),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 6,
                    Name = "Screws",
                    ProductNumber = 209031296,
                    QuantityRecieved = 200,
                    QuantityDefect = 10,
                    Description = "Screws for repairs.",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 4),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 7,
                    Name = "Rivets",
                    ProductNumber = 209031299,
                    QuantityRecieved = 100,
                    QuantityDefect = 40,
                    Description = "Rivets for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 5),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 8,
                    Name = "Washers",
                    ProductNumber = 244110399,
                    QuantityRecieved = 50,
                    QuantityDefect = 1,
                    Description = "Washers for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 5),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                      {
                    ID = 9,
                    Name = "Anchors",
                    ProductNumber = 204231293,
                    QuantityRecieved = 10,
                    QuantityDefect = 1,
                    Description = "Anchors for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 6),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 10,
                    Name = "Nails",
                    ProductNumber = 212031293,
                    QuantityRecieved = 200,
                    QuantityDefect = 54,
                    Description = "Nails for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 7),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                },
                new Part
                {
                    ID = 11,
                    Name = "Clips",
                    ProductNumber = 219031293,
                    QuantityRecieved = 200,
                    QuantityDefect = 29,
                    Description = "Clips for repairs",
                    Supplier = context.Suppliers.FirstOrDefault(c => c.ID == 7),
                    SAPNumber = 207956254,
                    PurchaseNumber = 4500730930,
                    SalesOrder = "Stock"
                });
                context.SaveChanges();
            }
            if (!context.Medias.Any())
            {
                //takes pictures turns into binary, then when pulled from
                //the database / model it returns them to original form. 

                //Cat pictures, remove later :(
                //byte[] demoPicture1 = File.ReadAllBytes("TemporaryImages/cat1.jpg");
                //byte[] demoPicture2 = File.ReadAllBytes("TemporaryImages/cat2.jpg");
                //byte[] demoPicture3 = File.ReadAllBytes("TemporaryImages/cat3.jpg");
                //byte[] demoPicture4 = File.ReadAllBytes("TemporaryImages/cat4.jpg");

                //Random Pictures of Defects
                byte[] badPaint1 = File.ReadAllBytes("TemporaryImages/badpaint.jpg");
                byte[] badPaint2 = File.ReadAllBytes("TemporaryImages/badpaint2.jpg");
                byte[] badWeld = File.ReadAllBytes("TemporaryImages/badWeld.jpg");
                byte[] bluePrint1 = File.ReadAllBytes("TemporaryImages/blueprint1.jpg");
                byte[] bluePrint2 = File.ReadAllBytes("TemporaryImages/blueprint2.jpg");
                byte[] faultybolt = File.ReadAllBytes("TemporaryImages/faultybolt.jpg");
                byte[] faultyrope = File.ReadAllBytes("TemporaryImages/faultyrope.jpg");
                byte[] faultyPrint = File.ReadAllBytes("TemporaryImages/tube.jpg");
                byte[] tube = File.ReadAllBytes("TemporaryImages/faultyPrint.jpg");

                //To have an NCR with multiple pictures assigned, simply create a new media
                //And Under Part, set it to the same ID, (see the first 2)..

                context.Medias.AddRange(
                    new Media
                    {
                        ID = 1,
                        Content = bluePrint1,
                        Description = "Faulty Blue Print 1",
                        MimeType = "image/jpg",
                        Links = "https://example.com/1bluePrint_video",
                        Part = context.Parts.FirstOrDefault(p=>p.ID == 1)
                    },
                    new Media
                    {
                        ID = 2,
                        Content = bluePrint2,
                        Description = "Faulty Blue Print 2",
                        MimeType = "image/jpg",
                        Links = "https://example.com/2bluePrint_video2",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 1)
                    },
                    new Media
                    {
                        ID = 3,
                        Content = badPaint1,
                        Description = "Bad Paint Finish 1",
                        MimeType = "image/jpg",
                        Links = "https://example.com/3badPaint_gif",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 2)
                    },
                    new Media
                    {
                        ID = 4,
                        Content = badPaint2,
                        Description = "Bad Paint Finish 2",
                        MimeType = "image/jpg",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 2)
                    },
                    new Media
                    {
                        ID = 5,
                        Content = badWeld,
                        Description = "Poorly done weld",
                        MimeType = "image/jpg",
                        Part = context.Parts.FirstOrDefault(p=>p.ID == 3)
                    },
                    new Media
                    {
                        ID = 6,
                        Content = faultybolt,
                        Description = "faulty bolt",
                        MimeType = "image/jpg",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 4)
                    },
                    new Media
                    {
                        ID = 7,
                        Content = faultyrope,
                        Description = "faulty rope",
                        MimeType = "image/jpg",
                        Links = "https://example.com/7faultyRope_video",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 5)
                    },
                    new Media
                    {
                        ID = 8,
                        Content = faultyPrint,
                        Description = "faulty print",
                        MimeType = "image/jpg",
                        Links = "https://example.com/8faultPrint_video",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 6)
                    },
                    new Media
                    {
                        ID = 9,
                        Content = tube,
                        Description = "Poor tube finish",
                        MimeType = "image/jpg",
                        Links = "https://example.com/9poorTubeFinish_video",
                        Part = context.Parts.FirstOrDefault(p => p.ID == 7)
                    }
                    );
                context.SaveChanges();
            }
            if (!context.Operations.Any())
            {
                //REPRESENTS A FORM NOT A PERSON! THERE HAS TO BE ONE To ONE WITH NCR. 10 NCRS = 10 PURCHASING 
                context.Operations.AddRange(
                new Operation
                {
                    ID = 1,
                    Name = "James Jones",
                    OperationDate = DateTime.Parse("2024-01-20"),
                    OperationDecision = (OperationDecision)1,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = true,
                },
                new Operation
                {
                    ID = 2,
                    Name = "Jane Little",
                    OperationDate = DateTime.Parse("2024-01-22"),
                    OperationDecision = (OperationDecision)1,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = false,
                },
                new Operation
                {
                    ID = 3,
                    Name = "Matt Turner",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = false,
                    OperationFollowUp = true,
                },
                new Operation
                {
                    ID = 4,
                    Name = "Sandy Roof",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = false,
                },
                new Operation
                {
                    ID = 5,
                    Name = "Alex Baxter",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = false,
                    OperationFollowUp = true,
                },
                new Operation
                {
                    ID = 6,
                    Name = "Sam Smith",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = false,
                },
                new Operation
                {
                    ID = 7,
                    Name = "Alex Baxter",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = false,
                },
                new Operation
                {
                    ID = 8,
                    Name = "Jame Scot",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = false,
                    OperationFollowUp = false,
                },
                new Operation
                {
                    ID = 9,
                    Name = "Matt Turner",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = true,
                },
                new Operation
                {
                    ID = 10,
                    Name = "Matt Turner",
                    OperationDate = DateTime.Parse("2024-01-24"),
                    OperationDecision = (OperationDecision)2,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = true,
                    OperationFollowUp = false,
                },
                new Operation
                {
                    ID = 11,
                    Name = "James Jones",
                    OperationDate = DateTime.Parse("2024-01-20"),
                    OperationDecision = (OperationDecision)4,
                    OperationNotes = "Fill Out With Real Data",
                    OperationCar = false,
                    OperationFollowUp = false,
                });
                
                context.SaveChanges();
            }
            if (!context.Engineerings.Any())
            {
                context.Engineerings.AddRange(
                new Engineering
                {
                    ID = 1,
                    CustomerNotify = false,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering suggested a change in the assembly process to improve efficiency",
                    RevisionOriginal = 12345,
                    RevisionUpdated = 12346,
                    RevisionDate = DateTime.Parse("2024-01-20"),
                    Name = "John Smith",
                    Date = DateTime.Parse("2024-01-20"),
                    EngineeringDisposition = 0
                },
                new Engineering
                {
                    ID = 2,
                    CustomerNotify = true,
                    DrawUpdate = true,
                    DispositionNotes = "Engineering instructed the fabrication team to use a higher-grade alloy for improved durability",
                    RevisionOriginal = 32434,
                    RevisionUpdated = 52123,
                    RevisionDate = DateTime.Parse("2024-01-22"),
                    Name = "James Brady",
                    Date = DateTime.Parse("2024-01-22"),
                    EngineeringDisposition = (EngineeringDisposition)1,
                },
                new Engineering
                {
                    ID = 3,
                    CustomerNotify = false,
                    DrawUpdate = true,
                    DispositionNotes = "Engineering revised the technical specifications to accommodate new performance requirements",
                    RevisionOriginal = 87645,
                    RevisionUpdated = 54632,
                    RevisionDate = DateTime.Parse("2024-01-24"),
                    Name = "Linda Johnson",
                    Date = DateTime.Parse("2024-01-24"),
                    EngineeringDisposition = (EngineeringDisposition)2,
                },
                new Engineering
                {
                    ID = 4,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering provided clarification on the tolerance requirements for precision machining",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 5,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering recommended a redesign of the structural support to enhance stability",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 6,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering advised increasing the thickness of the material to meet safety standards.",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 7,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering proposed integrating a new feature to enhance product functionality.",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 8,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "N/A",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 9,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering identified a potential flaw in the design and suggested a modification",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 10,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering conducted a feasibility study for implementing a cost-saving measure",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,
                },
                new Engineering
                {
                    ID = 11,
                    CustomerNotify = true,
                    DrawUpdate = false,
                    DispositionNotes = "Engineering insisted on creating a new way to solve this problem.",
                    RevisionOriginal = 09854,
                    RevisionUpdated = 23465,
                    RevisionDate = DateTime.Parse("2024-01-26"),
                    Name = "Luke Miller",
                    Date = DateTime.Parse("2024-01-26"),
                    EngineeringDisposition = (EngineeringDisposition)3,

                });
                context.SaveChanges();
            }
            if (!context.NCRs.Any())
            {
                context.NCRs.AddRange(
                new NCR
                {
                    ID = 1,
                    NCR_Number = "1",
                    NCR_Date = DateTime.Parse("2024-01-11"),
                    NCR_Status = true,
                    NCR_Stage = (NCRStage)1,       //NCR Stage can be changed to match the data 
                    Part = context.Parts.FirstOrDefault(p => p.ID == 1),
                    Engineering = context.Engineerings.FirstOrDefault(p=>p.ID == 1),
                    Operation = context.Operations.FirstOrDefault(p => p.ID == 1),
                    QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 1)
                },
                    new NCR
                    {
                        ID = 2,
                        NCR_Number = "2",
                        NCR_Date = DateTime.Parse("2024-01-12"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)1,   
                        Part = context.Parts.FirstOrDefault(p => p.ID == 2),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 2),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 2),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p=>p.ID ==2),   
                    },
                    new NCR
                    {
                        ID = 3,
                        NCR_Number = "3",
                        NCR_Date = DateTime.Parse("2024-01-15"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)2,      
                        Part = context.Parts.FirstOrDefault(p => p.ID == 3),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 3),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 3),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 3)
                    },
                    new NCR
                    {
                        ID = 4,
                        NCR_Number = "4",
                        NCR_Date = DateTime.Parse("2024-01-18"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)1,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 4),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 4),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 4),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 4)
                    },
                    new NCR
                    {
                        ID = 5,
                        NCR_Number = "5",
                        NCR_Date = DateTime.Parse("2024-01-18"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)2,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 5),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 5),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 5),
                           QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 5)
                    },
                    new NCR
                    {
                        ID = 6,
                        NCR_Number = "6",
                        NCR_Date = DateTime.Parse("2024-01-20"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)3,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 6),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 6),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 6),
                           QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 6)
                        
                    },
                    new NCR
                    {
                        ID = 7,
                        NCR_Number =  "7",
                        NCR_Date = DateTime.Parse("2024-01-22"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)1,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 7),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 7),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 7),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 7)
                    },
                    new NCR
                    {
                        ID = 8,
                        NCR_Number = "8",
                        NCR_Date = DateTime.Parse("2024-01-23"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)3,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 8),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 8),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 8),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 8)
                    },
                    new NCR
                    {
                        ID = 9,
                        NCR_Number = "9",
                        NCR_Date = DateTime.Parse("2024-01-24"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)1,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 9),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 9),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 9),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 9)
                    },
                    new NCR
                    {
                        ID = 10,
                        NCR_Number = "10",
                        NCR_Date = DateTime.Parse("2024-01-25"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)2,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 10),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 10),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 10),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 10)
                    },
                    new NCR
                    {
                        ID = 11,
                        NCR_Number = "11",
                        NCR_Date = DateTime.Parse("2024-01-26"),
                        NCR_Status = true,
                        NCR_Stage = (NCRStage)1,
                        Part = context.Parts.FirstOrDefault(p => p.ID == 11),
                        Engineering = context.Engineerings.FirstOrDefault(p => p.ID == 11),
                        Operation = context.Operations.FirstOrDefault(p => p.ID == 11),
                        QualityInspection = context.QualityInspections.FirstOrDefault(p => p.ID == 11)
                    });
      
                context.SaveChanges();
            }
          
            if (!context.FollowUps.Any())
            {
                context.FollowUps.AddRange(
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
                    new DefectList { DefectID = 1, PartID = 1},
                    new DefectList { DefectID = 2, PartID = 2},
                    new DefectList { DefectID = 3, PartID = 3},
                    new DefectList { DefectID = 4, PartID = 4},
                    new DefectList { DefectID = 5, PartID = 5},
                    new DefectList { DefectID = 6, PartID = 6},
                    new DefectList { DefectID = 7, PartID = 7},
                    new DefectList { DefectID = 8, PartID = 8},
                    new DefectList { DefectID = 9, PartID = 9},
                    new DefectList { DefectID = 10, PartID = 10},
                    new DefectList { DefectID = 11, PartID = 11},
                };
                context.DefectLists.AddRange(defectLists);
                context.SaveChanges();
            }
            if (!context.QualityInspections.Any())
            {
                context.QualityInspections.AddRange(
                    new QualityInspection
                    {
                        ID = 1,
                        Name = "John Smith",
                        Date = DateTime.Parse("2024-02-01"),
                        ItemMarked = true,
                        ReInspected = false,
                        QualityIdentify = (QualityIdentify)1
                        
                    },
                    new QualityInspection
                    {
                        ID = 2,
                        Name = "John Doe",
                        Date = DateTime.Parse("2024-01-23"),
                        ItemMarked = true,
                        ReInspected = true,
                    },
                    new QualityInspection
                    {
                        ID = 3,
                        Name = "Sam Jordan",
                        Date = DateTime.Parse("2024-02-02"),
                        ItemMarked = true,
                        ReInspected = false,
                    },
                    new QualityInspection
                    {
                        ID = 4,
                        Name = "Gregy Frog",
                        Date = DateTime.Parse("2024-01-12"),
                        ItemMarked = false,
                        ReInspected = false,
                    },
                    new QualityInspection
                    {
                        ID = 5,
                        Name = "Pillow Man",
                        Date = DateTime.Parse("2024-02-01"),
                        ItemMarked = true,
                        ReInspected = false,
                    });
            }

        }
    }
}