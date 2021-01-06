namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDateTimeToTestsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Datetime", c => c.String(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Tests", "Datetime");
        }
    }
}
