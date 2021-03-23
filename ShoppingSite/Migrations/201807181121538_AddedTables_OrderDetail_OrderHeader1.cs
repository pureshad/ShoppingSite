namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedTables_OrderDetail_OrderHeader1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        MenuItemId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        OrderHeader_Id = c.Int(),
                        Products_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderHeaders", t => t.OrderHeader_Id)
                .ForeignKey("dbo.Products", t => t.Products_Id)
                .Index(t => t.OrderHeader_Id)
                .Index(t => t.Products_Id);
            
            CreateTable(
                "dbo.OrderHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        OrderTotal = c.Double(nullable: false),
                        Comments = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders");
            DropForeignKey("dbo.OrderHeaders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OrderHeaders", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Products_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_Id" });
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
        }
    }
}
