namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FKOrderId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders");
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_Id" });
            DropColumn("dbo.OrderDetails", "OrderId");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderHeader_Id", newName: "OrderId");
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.OrderHeaders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.OrderHeaders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "OrderHeader_Id");
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderHeader_Id");
            AddForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders", "Id");
        }
    }
}
