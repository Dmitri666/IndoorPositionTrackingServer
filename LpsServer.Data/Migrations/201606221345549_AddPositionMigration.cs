namespace LpsServer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPositionMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusinessHours", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Favorits", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomTable", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Position", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Rating", "RoomId", "dbo.Room");
            DropForeignKey("dbo.KitchenMenu", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Photo", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchenInternational", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchen", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Specialization", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Beacon", "Room_Id", "dbo.Room");
            DropPrimaryKey("dbo.Room");
            CreateTable(
                "dbo.PositionLog",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RoomId = c.Guid(nullable: false),
                        DeviceId = c.String(),
                        Key1 = c.Int(nullable: false),
                        Key2 = c.Int(nullable: false),
                        Key3 = c.Int(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Room", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Room", "Id");
            AddForeignKey("dbo.BusinessHours", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Favorits", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomTable", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Position", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rating", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KitchenMenu", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Photo", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomKitchenInternational", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomKitchen", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Specialization", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Beacon", "Room_Id", "dbo.Room", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Beacon", "Room_Id", "dbo.Room");
            DropForeignKey("dbo.Specialization", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchen", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomKitchenInternational", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Photo", "RoomId", "dbo.Room");
            DropForeignKey("dbo.KitchenMenu", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Rating", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Position", "RoomId", "dbo.Room");
            DropForeignKey("dbo.RoomTable", "RoomId", "dbo.Room");
            DropForeignKey("dbo.Favorits", "RoomId", "dbo.Room");
            DropForeignKey("dbo.BusinessHours", "RoomId", "dbo.Room");
            DropPrimaryKey("dbo.Room");
            AlterColumn("dbo.Room", "Id", c => c.Guid(nullable: false, identity: true));
            DropTable("dbo.PositionLog");
            AddPrimaryKey("dbo.Room", "Id");
            AddForeignKey("dbo.Beacon", "Room_Id", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Specialization", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomKitchen", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomKitchenInternational", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Photo", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.KitchenMenu", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rating", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Position", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoomTable", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Favorits", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BusinessHours", "RoomId", "dbo.Room", "Id", cascadeDelete: true);
        }
    }
}
