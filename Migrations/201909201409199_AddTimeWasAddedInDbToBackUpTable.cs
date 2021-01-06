namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTimeWasAddedInDbToBackUpTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_BackupTests", "dateWasAdded", c => c.String(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.tb_BackupTests", "dateWasAdded");
        }
    }
}
