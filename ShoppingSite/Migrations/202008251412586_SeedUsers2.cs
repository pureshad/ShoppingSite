namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers2 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                                        
                      INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fcf65137-4293-4a5f-950b-da02257b7933', N'761158ef-f972-4950-9f35-90856536426d')
                ");
        }

        public override void Down()
        {
        }
    }
}
