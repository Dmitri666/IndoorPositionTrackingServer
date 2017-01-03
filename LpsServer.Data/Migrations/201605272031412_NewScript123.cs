namespace LpsServer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewScript123 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeaconInRange",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Identifier1 = c.Guid(nullable: false),
                        Identifier2 = c.Int(nullable: false),
                        Identifier3 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Beacon",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Identifier1 = c.Guid(nullable: false),
                        Identifier2 = c.Int(nullable: false),
                        Identifier3 = c.Int(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Room_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Identifier3, name: "IX_Identifier3Room")
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RoomHeight = c.Single(nullable: false),
                        RoomWidth = c.Single(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Location = c.Geography(),
                        Street = c.String(maxLength: 50),
                        StreetNumber = c.String(maxLength: 10),
                        City = c.String(maxLength: 20),
                        Plz = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Homepage = c.String(maxLength: 50),
                        Time = c.DateTime(nullable: false),
                        CanvasImage = c.String(maxLength: 200),
                        Description = c.String(),
                        IsVisibleBusinessHours = c.Boolean(nullable: false),
                        IsChatExist = c.Boolean(nullable: false),
                        SvgLayout = c.String(),
                        JsonModel = c.String(),
                        TableLayout = c.String(),
                        LayoutZoom = c.Single(nullable: false),
                        RealScaleFactor = c.Single(nullable: false),
                        BackgroungImage = c.String(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BusinessHours",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        OpenTime = c.DateTime(nullable: false),
                        CloseTime = c.DateTime(nullable: false),
                        PauseStart = c.DateTime(),
                        PauseEnd = c.DateTime(),
                        Close = c.Boolean(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Favorits",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RoomId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 15),
                        Company = c.String(maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 15),
                        Photo = c.String(maxLength: 200),
                        DeviceId = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        State = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        PeopleCount = c.Int(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.BookingRoomTable",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BookingId = c.Guid(nullable: false),
                        RoomTableId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.RoomTable", t => t.RoomTableId)
                .Index(t => t.BookingId)
                .Index(t => t.RoomTableId);
            
            CreateTable(
                "dbo.RoomTable",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Angle = c.Double(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Description = c.String(),
                        Type = c.String(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.ConversationMessage",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Message = c.String(),
                        Time = c.DateTime(nullable: false),
                        Conversation_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversation", t => t.Conversation_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Conversation_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Conversation",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        State = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        User1_Id = c.Guid(nullable: false),
                        User2_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User1_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User2_Id)
                .Index(t => t.User1_Id)
                .Index(t => t.User2_Id);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceId = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        DeviceId = c.String(nullable: false, maxLength: 30),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.DeviceId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.UserPhoto",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        Image = c.String(nullable: false, maxLength: 200),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RoleTypeId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleType", t => t.RoleTypeId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleTypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RoleType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Order = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitchenMenu",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.String(nullable: false),
                        Description = c.String(maxLength: 300),
                        Order = c.Int(nullable: false),
                        KitchenMenuTypeId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KitchenMenuType", t => t.KitchenMenuTypeId)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.KitchenMenuTypeId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.KitchenMenuType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 300),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        Image = c.String(nullable: false, maxLength: 200),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.RoomKitchenInternational",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        KitchenInternationalTypeId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KitchenInternationalType", t => t.KitchenInternationalTypeId)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.KitchenInternationalTypeId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.KitchenInternationalType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ParentId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 300),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomKitchen",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        KitchenTypeId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KitchenType", t => t.KitchenTypeId)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.KitchenTypeId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.KitchenType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 300),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specialization",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RoomId = c.Guid(nullable: false),
                        SpecializationTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.SpecializationType", t => t.SpecializationTypeId)
                .Index(t => t.RoomId)
                .Index(t => t.SpecializationTypeId);
            
            CreateTable(
                "dbo.SpecializationType",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ParentId = c.Guid(),
                        Hierarchie = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 300),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logging",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Message = c.String(),
                        Data = c.String(),
                        InnerException = c.String(),
                        Time = c.DateTime(nullable: false),
                        InputData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Beacon", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Room", "UserId", "dbo.User");
            DropForeignKey("dbo.Specialization", "SpecializationTypeId", "dbo.SpecializationType");
            DropForeignKey("dbo.Specialization", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchen", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchen", "KitchenTypeId", "dbo.KitchenType");
            DropForeignKey("dbo.RoomKitchenInternational", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchenInternational", "KitchenInternationalTypeId", "dbo.KitchenInternationalType");
            DropForeignKey("dbo.Photo", "RoomId", "dbo.Room");
            DropForeignKey("dbo.KitchenMenu", "RoomId", "dbo.Room");
            DropForeignKey("dbo.KitchenMenu", "KitchenMenuTypeId", "dbo.KitchenMenuType");
            DropForeignKey("dbo.Favorits", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleTypeId", "dbo.RoleType");
            DropForeignKey("dbo.Rating", "UserId", "dbo.User");
            DropForeignKey("dbo.Rating", "RoomId", "dbo.Room");
            DropForeignKey("dbo.UserPhoto", "RoomId", "dbo.User");
            DropForeignKey("dbo.User", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.Position", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Position", "DeviceId", "dbo.Device");
            DropForeignKey("dbo.ConversationMessage", "User_Id", "dbo.User");
            DropForeignKey("dbo.ConversationMessage", "Conversation_Id", "dbo.Conversation");
            DropForeignKey("dbo.Conversation", "User2_Id", "dbo.User");
            DropForeignKey("dbo.Conversation", "User1_Id", "dbo.User");
            DropForeignKey("dbo.Booking", "User_Id", "dbo.User");
            DropForeignKey("dbo.BookingRoomTable", "RoomTableId", "dbo.RoomTable");
            DropForeignKey("dbo.RoomTable", "RoomId", "dbo.Room");
            DropForeignKey("dbo.BookingRoomTable", "BookingId", "dbo.Booking");
            DropForeignKey("dbo.Favorits", "RoomId", "dbo.Room");
            DropForeignKey("dbo.BusinessHours", "RoomId", "dbo.Room");
            DropIndex("dbo.Specialization", new[] { "SpecializationTypeId" });
            DropIndex("dbo.Specialization", new[] { "RoomId" });
            DropIndex("dbo.RoomKitchen", new[] { "RoomId" });
            DropIndex("dbo.RoomKitchen", new[] { "KitchenTypeId" });
            DropIndex("dbo.RoomKitchenInternational", new[] { "RoomId" });
            DropIndex("dbo.RoomKitchenInternational", new[] { "KitchenInternationalTypeId" });
            DropIndex("dbo.Photo", new[] { "RoomId" });
            DropIndex("dbo.KitchenMenu", new[] { "RoomId" });
            DropIndex("dbo.KitchenMenu", new[] { "KitchenMenuTypeId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleTypeId" });
            DropIndex("dbo.Rating", new[] { "UserId" });
            DropIndex("dbo.Rating", new[] { "RoomId" });
            DropIndex("dbo.UserPhoto", new[] { "RoomId" });
            DropIndex("dbo.Position", new[] { "RoomId" });
            DropIndex("dbo.Position", new[] { "DeviceId" });
            DropIndex("dbo.Conversation", new[] { "User2_Id" });
            DropIndex("dbo.Conversation", new[] { "User1_Id" });
            DropIndex("dbo.ConversationMessage", new[] { "User_Id" });
            DropIndex("dbo.ConversationMessage", new[] { "Conversation_Id" });
            DropIndex("dbo.RoomTable", new[] { "RoomId" });
            DropIndex("dbo.BookingRoomTable", new[] { "RoomTableId" });
            DropIndex("dbo.BookingRoomTable", new[] { "BookingId" });
            DropIndex("dbo.Booking", new[] { "User_Id" });
            DropIndex("dbo.User", new[] { "DeviceId" });
            DropIndex("dbo.User", new[] { "Name" });
            DropIndex("dbo.Favorits", new[] { "UserId" });
            DropIndex("dbo.Favorits", new[] { "RoomId" });
            DropIndex("dbo.BusinessHours", new[] { "RoomId" });
            DropIndex("dbo.Room", new[] { "UserId" });
            DropIndex("dbo.Beacon", new[] { "Room_Id" });
            DropIndex("dbo.Beacon", "IX_Identifier3Room");
            DropTable("dbo.Logging");
            DropTable("dbo.SpecializationType");
            DropTable("dbo.Specialization");
            DropTable("dbo.KitchenType");
            DropTable("dbo.RoomKitchen");
            DropTable("dbo.KitchenInternationalType");
            DropTable("dbo.RoomKitchenInternational");
            DropTable("dbo.Photo");
            DropTable("dbo.KitchenMenuType");
            DropTable("dbo.KitchenMenu");
            DropTable("dbo.RoleType");
            DropTable("dbo.UserRole");
            DropTable("dbo.Rating");
            DropTable("dbo.UserPhoto");
            DropTable("dbo.Position");
            DropTable("dbo.Device");
            DropTable("dbo.Conversation");
            DropTable("dbo.ConversationMessage");
            DropTable("dbo.RoomTable");
            DropTable("dbo.BookingRoomTable");
            DropTable("dbo.Booking");
            DropTable("dbo.User");
            DropTable("dbo.Favorits");
            DropTable("dbo.BusinessHours");
            DropTable("dbo.Room");
            DropTable("dbo.Beacon");
            DropTable("dbo.BeaconInRange");
        }
    }
}
