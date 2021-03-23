namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FKUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderHeaders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrderHeaders", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.OrderHeaders", "UserId");
            RenameColumn(table: "dbo.OrderHeaders", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.OrderHeaders", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.OrderHeaders", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.OrderHeaders", "UserId");
            AddForeignKey("dbo.OrderHeaders", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderHeaders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.OrderHeaders", new[] { "UserId" });
            AlterColumn("dbo.OrderHeaders", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderHeaders", "UserId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.OrderHeaders", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.OrderHeaders", "UserId", c => c.String(nullable: false));
            CreateIndex("dbo.OrderHeaders", "ApplicationUser_Id");
            AddForeignKey("dbo.OrderHeaders", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
