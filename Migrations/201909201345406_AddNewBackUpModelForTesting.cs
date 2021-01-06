namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNewBackUpModelForTesting : DbMigration
    {
        public override void Up()
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
                    patients_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.patients_Id, cascadeDelete: true)
                .Index(t => t.patients_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.tb_BackupTests", "patients_Id", "dbo.Patients");
            DropIndex("dbo.tb_BackupTests", new[] { "patients_Id" });
            DropTable("dbo.tb_BackupTests");
        }
    }
}
