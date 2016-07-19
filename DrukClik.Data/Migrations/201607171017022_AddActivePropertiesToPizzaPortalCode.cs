namespace DrukClik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivePropertiesToPizzaPortalCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PizzaPortalCodes", "active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PizzaPortalCodes", "active");
        }
    }
}
