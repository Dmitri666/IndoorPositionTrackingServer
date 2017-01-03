namespace LpsServer.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LpsServer.Data.LpsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LpsServer.Data.LpsContext context)
        {
            new FillDatabase(context).Load();
        }
    }
}
