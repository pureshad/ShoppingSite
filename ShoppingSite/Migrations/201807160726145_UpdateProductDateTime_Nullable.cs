namespace ShoppingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductDateTime_Nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CreatedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "CreatedTime", c => c.DateTime(nullable: false));
        }
    }
}
