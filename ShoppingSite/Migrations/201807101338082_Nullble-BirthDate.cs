namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NullbleBirthDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
