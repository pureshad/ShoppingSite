namespace ShoppingSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2b4f2b78-8f74-46ce-af2b-0745c4bc5e6e', N'admin@gmail.com', 0, N'AB62mGfognAuXtzA/STYrDf4rrPLdONqmHgdBQes8VqFIdQURejDW9RxtMdelrQv+g==', N'703e084e-652b-4588-86bc-57c24762e378', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')
                    
                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'761158ef-f972-4950-9f35-90856536426d', N'IsAdmin')
                    
                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2b4f2b78-8f74-46ce-af2b-0745c4bc5e6e', N'761158ef-f972-4950-9f35-90856536426d')
                ");
        }

        public override void Down()
        {
        }
    }
}
