namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserTypeInUsersModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "lab", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "ray", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ray");
            DropColumn("dbo.Users", "lab");
        }
    }
}
