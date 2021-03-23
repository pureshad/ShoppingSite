namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addedFK_ProductId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Products_Id", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "Products_Id" });
            RenameColumn(table: "dbo.OrderDetails", name: "Products_Id", newName: "ProductsId");
            AlterColumn("dbo.OrderDetails", "ProductsId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "ProductsId");
            AddForeignKey("dbo.OrderDetails", "ProductsId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "MenuItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "MenuItemId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "ProductsId", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "ProductsId" });
            AlterColumn("dbo.OrderDetails", "ProductsId", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "ProductsId", newName: "Products_Id");
            CreateIndex("dbo.OrderDetails", "Products_Id");
            AddForeignKey("dbo.OrderDetails", "Products_Id", "dbo.Products", "Id");
        }
    }
}
