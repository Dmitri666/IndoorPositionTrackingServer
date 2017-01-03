namespace LpsServer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewScript33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "LoginProvider", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "LoginProvider");
        }
    }
}
