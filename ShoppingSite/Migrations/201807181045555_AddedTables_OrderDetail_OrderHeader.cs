namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedTables_OrderDetail_OrderHeader : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCarts", "UserId", "dbo.Users");
            DropIndex("dbo.ShoppingCarts", new[] { "UserId" });
            AddColumn("dbo.ShoppingCarts", "ApplicationUserId", c => c.String(nullable: false));
            AddColumn("dbo.ShoppingCarts", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.ShoppingCarts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCarts", "Name", c => c.String(nullable: false));
            DropColumn("dbo.ShoppingCarts", "Count");
            DropColumn("dbo.ShoppingCarts", "ApplicationUserId");
            CreateIndex("dbo.ShoppingCarts", "UserId");
            AddForeignKey("dbo.ShoppingCarts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
