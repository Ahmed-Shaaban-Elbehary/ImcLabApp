namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateThreeBackUpModelForTests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_labsBackUp",
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
            
            CreateTable(
                "dbo.tb_radiosBackUp",
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
            
            CreateTable(
                "dbo.tb_TumorsBackUp",
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
        
        public override void Down()
        {
            DropTable("dbo.tb_TumorsBackUp");
            DropTable("dbo.tb_radiosBackUp");
            DropTable("dbo.tb_labsBackUp");
        }
    }
}
