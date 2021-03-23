namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrderHeader_AddedColumnStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderHeaders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderHeaders", "Status");
        }
    }
}
