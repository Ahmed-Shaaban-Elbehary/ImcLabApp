namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBackUpRequestsSystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestesBackUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        medicalNumber = c.String(nullable: false),
                        nationalId = c.String(nullable: false),
                        dateTime = c.String(nullable: false),
                        RemovedDate = c.String(nullable: false),
                        Type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RequestesBackUps");
        }
    }
}
