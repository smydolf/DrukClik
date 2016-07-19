namespace DrukClik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGmailAuthTable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GmailSmtpAuths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        Login = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GmailSmtpAuths");
        }
    }
}
