namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestsTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "lb_Request", c => c.Boolean(nullable: false));
            AddColumn("dbo.Requests", "rd_Request", c => c.Boolean(nullable: false));
            AddColumn("dbo.Requests", "tr_Request", c => c.Boolean(nullable: false));
            DropColumn("dbo.Requests", "testType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "testType", c => c.String(nullable: false));
            DropColumn("dbo.Requests", "tr_Request");
            DropColumn("dbo.Requests", "rd_Request");
            DropColumn("dbo.Requests", "lb_Request");
        }
    }
}
