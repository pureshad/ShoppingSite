namespace ShoppingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryType_DropColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryTypes", "Name", c => c.String(nullable: false));
            DropColumn("dbo.CategoryTypes", "Luxury");
            DropColumn("dbo.CategoryTypes", "Fashion");
            DropColumn("dbo.CategoryTypes", "Kids");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CategoryTypes", "Kids", c => c.String(nullable: false));
            AddColumn("dbo.CategoryTypes", "Fashion", c => c.String(nullable: false));
            AddColumn("dbo.CategoryTypes", "Luxury", c => c.String(nullable: false));
            DropColumn("dbo.CategoryTypes", "Name");
        }
    }
}
