namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingUserNameToBAckUpTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_BackupTests", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.tb_BackupTests", "PatientName", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.tb_BackupTests", "PatientName", c => c.String());
            DropColumn("dbo.tb_BackupTests", "UserName");
        }
    }
}
