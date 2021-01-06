namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserRoles : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "UserRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserRole", c => c.String(nullable: false));
        }
    }
}
