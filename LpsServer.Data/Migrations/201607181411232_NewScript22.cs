namespace LpsServer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewScript22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ProviderKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ProviderKey");
        }
    }
}
