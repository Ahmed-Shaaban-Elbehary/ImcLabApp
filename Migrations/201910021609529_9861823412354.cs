namespace ImcLabApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9861823412354 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tests", newName: "Labs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Labs", newName: "Tests");
        }
    }
}
