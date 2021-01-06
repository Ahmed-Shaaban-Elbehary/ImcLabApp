namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNationIDToPatientModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "NationalID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "NationalID");
        }
    }
}
