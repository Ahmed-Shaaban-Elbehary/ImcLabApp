namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false),
                    Password = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Patients",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    medicalNumber = c.String(nullable: false),
                    userName = c.String(nullable: false),
                    phoneNumber = c.String(nullable: false, maxLength: 11),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Tests",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    testName = c.String(nullable: false),
                    fileName = c.String(nullable: false),
                    fileUrl = c.String(nullable: false),
                    patients_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.patients_Id, cascadeDelete: true)
                .Index(t => t.patients_Id);

            CreateTable(
                "dbo.PatientsRegisterations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false),
                    Password = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false),
                    Password = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Tests", "patients_Id", "dbo.Patients");
            DropIndex("dbo.Tests", new[] { "patients_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.PatientsRegisterations");
            DropTable("dbo.Tests");
            DropTable("dbo.Patients");
            DropTable("dbo.Admins");
        }
    }
}
