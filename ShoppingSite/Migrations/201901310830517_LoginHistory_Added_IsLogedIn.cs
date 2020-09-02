namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class LoginHistory_Added_IsLogedIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginHistories", "IsLogedIn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginHistories", "IsLogedIn");
        }
    }
}
