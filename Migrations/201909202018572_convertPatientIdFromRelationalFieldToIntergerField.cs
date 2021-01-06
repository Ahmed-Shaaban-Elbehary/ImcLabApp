namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class convertPatientIdFromRelationalFieldToIntergerField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_BackupTests", "patients_Id", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.tb_BackupTests", "patients_Id");
        }
    }
}
