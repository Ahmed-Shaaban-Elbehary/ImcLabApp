namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDepartmentsInUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Departments", c => c.String(nullable: false));
            DropColumn("dbo.Users", "lab");
            DropColumn("dbo.Users", "ray");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ray", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "lab", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Departments");
        }
    }
}
