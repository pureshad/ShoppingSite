namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init_LoginHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginTime = c.DateTime(),
                        LogoutTime = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginHistories");
        }
    }
}
