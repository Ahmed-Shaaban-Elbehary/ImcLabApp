namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRequestsbackUpTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestesBackUps", "UserName", c => c.String(nullable: false));
            AddColumn("dbo.RequestesBackUps", "lb_Request", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestesBackUps", "rd_Request", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequestesBackUps", "tr_Request", c => c.Boolean(nullable: false));
            DropColumn("dbo.RequestesBackUps", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestesBackUps", "Type", c => c.Boolean(nullable: false));
            DropColumn("dbo.RequestesBackUps", "tr_Request");
            DropColumn("dbo.RequestesBackUps", "rd_Request");
            DropColumn("dbo.RequestesBackUps", "lb_Request");
            DropColumn("dbo.RequestesBackUps", "UserName");
        }
    }
}
