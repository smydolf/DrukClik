namespace DrukClik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSendEmailError : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SendEmailErrors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ErrorMessage = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        FormEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormEntities", t => t.FormEntity_Id)
                .Index(t => t.FormEntity_Id);
        }    
        public override void Down()
        {
            DropForeignKey("dbo.SendEmailErrors", "FormEntity_Id", "dbo.FormEntities");
            DropIndex("dbo.SendEmailErrors", new[] { "FormEntity_Id" });
            DropTable("dbo.SendEmailErrors");
        }
    }
}
