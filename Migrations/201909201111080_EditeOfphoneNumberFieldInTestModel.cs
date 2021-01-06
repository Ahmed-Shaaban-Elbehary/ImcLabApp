namespace ImcLabApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditeOfphoneNumberFieldInTestModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "phoneNumber", c => c.String(nullable: false, maxLength: 20));
        }

        public override void Down()
        {
            AlterColumn("dbo.Patients", "phoneNumber", c => c.String(nullable: false, maxLength: 11));
        }
    }
}
