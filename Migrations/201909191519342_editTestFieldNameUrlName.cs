namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class editTestFieldNameUrlName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Tests", "urlName", c => c.String(nullable: false));
            DropColumn("dbo.Tests", "fileName");
            DropColumn("dbo.Tests", "fileUrl");
        }

        public override void Down()
        {
            AddColumn("dbo.Tests", "fileUrl", c => c.String(nullable: false));
            AddColumn("dbo.Tests", "fileName", c => c.String(nullable: false));
            DropColumn("dbo.Tests", "urlName");
            DropColumn("dbo.Tests", "Name");
        }
    }
}
