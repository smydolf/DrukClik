namespace DrukClik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSendEmailErrorErrorMessage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SendEmailErrors", "ErrorMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SendEmailErrors", "ErrorMessage", c => c.String());
        }
    }
}
