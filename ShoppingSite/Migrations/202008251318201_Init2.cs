namespace ShoppingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.ShoppingCarts", "UserId", "dbo.Users");
            //DropIndex("dbo.ShoppingCarts", new[] { "UserId" });
            //CreateTable(
            //    "dbo.LoginHistories",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            LoginTime = c.DateTime(),
            //            LogoutTime = c.DateTime(),
            //            IsLogedIn = c.Boolean(nullable: false),
            //            UserId = c.String(maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.OrderDetails",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            OrderId = c.Int(nullable: false),
            //            ProductsId = c.Int(nullable: false),
            //            Count = c.Int(nullable: false),
            //            Name = c.String(),
            //            Description = c.String(),
            //            Price = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.OrderHeaders", t => t.OrderId, cascadeDelete: true)
            //    .ForeignKey("dbo.Products", t => t.ProductsId, cascadeDelete: true)
            //    .Index(t => t.OrderId)
            //    .Index(t => t.ProductsId);
            
            //CreateTable(
            //    "dbo.OrderHeaders",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            OrderDate = c.DateTime(nullable: false),
            //            OrderTotal = c.Double(nullable: false),
            //            Comments = c.String(),
            //            Status = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //AddColumn("dbo.ShoppingCarts", "ApplicationUserId", c => c.String(nullable: false));
            //AddColumn("dbo.ShoppingCarts", "Count", c => c.Int(nullable: false));
            //AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 255));
            //AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 255));
            //AddColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false, maxLength: 255));
            //AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
            //DropColumn("dbo.ShoppingCarts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCarts", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.OrderDetails", "ProductsId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.OrderHeaders");
            DropForeignKey("dbo.OrderHeaders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.OrderHeaders", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductsId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.ShoppingCarts", "Count");
            DropColumn("dbo.ShoppingCarts", "ApplicationUserId");
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.LoginHistories");
            CreateIndex("dbo.ShoppingCarts", "UserId");
            AddForeignKey("dbo.ShoppingCarts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
