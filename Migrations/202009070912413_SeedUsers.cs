namespace MovCustMVCAppWithAuthen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7af720df-7d14-40c0-a665-2cf3917758ec', N'guest@gmail.com', 0, N'ABK8rzjSDEraebQgU1cdUP1sX/soggiQCNeJEg+NApmNWH+sWRrXn978gOplRbTOBg==', N'd6572bec-7728-4a7c-985a-c824b37315d2', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7eee3470-9e42-4f25-ba45-d5f58f437b1c', N'admin1user@gmail.com', 0, N'AOBJPGGxUgf2rkHHOzWud4aVBwdpsmvSz+rLhNU1BPbydf8ESwdyTAQPWu4Q6MipAA==', N'30c85d49-f613-409a-8905-c9044f23a287', NULL, 0, 0, NULL, 1, 0, N'admin1user@gmail.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'339cd084-126c-4eb8-88e5-cc5d8a0bd85b', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7eee3470-9e42-4f25-ba45-d5f58f437b1c', N'339cd084-126c-4eb8-88e5-cc5d8a0bd85b')


");
        }
        
        public override void Down()
        {
        }
    }
}
