namespace ShoppingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableCategoryType_AddNewColumns_Products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Luxury = c.String(nullable: false),
                        Fashion = c.String(nullable: false),
                        Kids = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "StrapQuality", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Brand", c => c.String(nullable: false));
            AddColumn("dbo.Products", "DisplayType", c => c.String(nullable: false));
            AddColumn("dbo.Products", "IsAvailableInStock", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "NumberAvailable", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "CategoryTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryTypeId");
            AddForeignKey("dbo.Products", "CategoryTypeId", "dbo.CategoryTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryTypeId", "dbo.CategoryTypes");
            DropIndex("dbo.Products", new[] { "CategoryTypeId" });
            DropColumn("dbo.Products", "CategoryTypeId");
            DropColumn("dbo.Products", "NumberAvailable");
            DropColumn("dbo.Products", "IsAvailableInStock");
            DropColumn("dbo.Products", "DisplayType");
            DropColumn("dbo.Products", "Brand");
            DropColumn("dbo.Products", "StrapQuality");
            DropColumn("dbo.Products", "Price");
            DropTable("dbo.CategoryTypes");
        }
    }
}
