namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIdDoneField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "isDone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "isDone", c => c.Boolean(nullable: false));
        }
    }
}
