namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTumorsAndRadiosModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Radios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        testName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        urlName = c.String(nullable: false),
                        Datetime = c.String(nullable: false),
                        patients_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.patients_Id, cascadeDelete: true)
                .Index(t => t.patients_Id);
            
            CreateTable(
                "dbo.Tumors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        testName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        urlName = c.String(nullable: false),
                        Datetime = c.String(nullable: false),
                        patients_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.patients_Id, cascadeDelete: true)
                .Index(t => t.patients_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tumors", "patients_Id", "dbo.Patients");
            DropForeignKey("dbo.Radios", "patients_Id", "dbo.Patients");
            DropIndex("dbo.Tumors", new[] { "patients_Id" });
            DropIndex("dbo.Radios", new[] { "patients_Id" });
            DropTable("dbo.Tumors");
            DropTable("dbo.Radios");
        }
    }
}
