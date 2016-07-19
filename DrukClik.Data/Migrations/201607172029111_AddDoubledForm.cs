namespace DrukClik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoubledForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SendCodeLogs", "IsDouble", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SendCodeLogs", "IsDouble");
        }
    }
}
