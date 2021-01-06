namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removetb_BackUpTests : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.tb_BackupTests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tb_BackupTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        testName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        urlName = c.String(nullable: false),
                        Datetime = c.String(nullable: false),
                        dateWasAdded = c.String(nullable: false),
                        PatientMedicalNumber = c.String(nullable: false),
                        PatientName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        patients_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
