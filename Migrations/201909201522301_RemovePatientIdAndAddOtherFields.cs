namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovePatientIdAndAddOtherFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_BackupTests", "patients_Id", "dbo.Patients");
            DropIndex("dbo.tb_BackupTests", new[] { "patients_Id" });
            AddColumn("dbo.tb_BackupTests", "PatientMedicalNumber", c => c.String(nullable: false));
            AddColumn("dbo.tb_BackupTests", "PatientName", c => c.String());
            DropColumn("dbo.tb_BackupTests", "patients_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.tb_BackupTests", "patients_Id", c => c.Int(nullable: false));
            DropColumn("dbo.tb_BackupTests", "PatientName");
            DropColumn("dbo.tb_BackupTests", "PatientMedicalNumber");
            CreateIndex("dbo.tb_BackupTests", "patients_Id");
            AddForeignKey("dbo.tb_BackupTests", "patients_Id", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
