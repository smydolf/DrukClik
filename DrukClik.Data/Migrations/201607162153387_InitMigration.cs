namespace DrukClik.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Q1FilledDateTime = c.DateTime(nullable: false),
                        Q2AgeRange = c.String(),
                        Q3Sex = c.String(),
                        Q4City = c.String(),
                        Q5Profession = c.String(),
                        Q6FrequencyOfUse = c.String(),
                        Q7PrintedPage = c.String(),
                        Q8DataSafety = c.String(),
                        Q9PayAttentionFor = c.String(),
                        Q10KnowOrNotAnnonymusPrintingHouse = c.String(),
                        Q11SpentTime = c.String(),
                        Q12KeepingDateInCloud = c.String(),
                        Q13PercentKeepingDateInCloud = c.String(),
                        Q14SendDataViaEmail = c.String(),
                        Q15PercentSendDataViaEmail = c.String(),
                        Q16SatisfactionPercentSendDataViaEmail = c.String(),
                        Q17SpentMoneyForPrinting = c.String(),
                        Q18Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DownloadingDateTime = c.DateTime(nullable: false),
                        DownloadingTimeSpan = c.Time(nullable: false, precision: 7),
                        NewPosts = c.Int(nullable: false),
                        PostsWithEmail = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PizzaPortalCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SendCodeLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SendCodeDateTime = c.DateTime(nullable: false),
                        Email = c.String(),
                        FormEntity_Id = c.Int(),
                        PizzaPortalCode_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormEntities", t => t.FormEntity_Id)
                .ForeignKey("dbo.PizzaPortalCodes", t => t.PizzaPortalCode_Id)
                .Index(t => t.FormEntity_Id)
                .Index(t => t.PizzaPortalCode_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SendCodeLogs", "PizzaPortalCode_Id", "dbo.PizzaPortalCodes");
            DropForeignKey("dbo.SendCodeLogs", "FormEntity_Id", "dbo.FormEntities");
            DropIndex("dbo.SendCodeLogs", new[] { "PizzaPortalCode_Id" });
            DropIndex("dbo.SendCodeLogs", new[] { "FormEntity_Id" });
            DropTable("dbo.SendCodeLogs");
            DropTable("dbo.PizzaPortalCodes");
            DropTable("dbo.Logs");
            DropTable("dbo.FormEntities");
        }
    }
}
