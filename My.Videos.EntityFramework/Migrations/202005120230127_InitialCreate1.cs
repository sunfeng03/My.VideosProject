namespace My.Videos.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(maxLength: 2000),
                        LogType = c.Int(nullable: false),
                        ClassName = c.String(maxLength: 500, unicode: false),
                        ApplicationId = c.String(maxLength: 50, unicode: false),
                        CreateName = c.String(maxLength: 50, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.tblMenu", "Name", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenu", "MenuURL", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.tblMenu", "FontClass", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenu", "CreateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenu", "UpdateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenuRelationRole", "RoleName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenuRelationRole", "MenuId", c => c.Int(nullable: false));
            AlterColumn("dbo.tblMenuRelationRole", "MenuName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenuRelationRole", "CreateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblMenuRelationRole", "UpdateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblRole", "RoleName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblRole", "CreateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblRole", "UpdateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "UserId", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "UserName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "Email", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "MobilePhone", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "NickName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "Password", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.tblUser", "CreateName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.tblUser", "UpdateName", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblUser", "UpdateName", c => c.String(maxLength: 50));
            AlterColumn("dbo.tblUser", "CreateName", c => c.String(maxLength: 50));
            AlterColumn("dbo.tblUser", "Password", c => c.String(maxLength: 100));
            AlterColumn("dbo.tblUser", "NickName", c => c.String(maxLength: 50));
            AlterColumn("dbo.tblUser", "MobilePhone", c => c.String(maxLength: 50));
            AlterColumn("dbo.tblUser", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.tblUser", "UserName", c => c.String(maxLength: 50));
            AlterColumn("dbo.tblUser", "UserId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.tblRole", "UpdateName", c => c.String());
            AlterColumn("dbo.tblRole", "CreateName", c => c.String());
            AlterColumn("dbo.tblRole", "RoleName", c => c.String(maxLength: 10));
            AlterColumn("dbo.tblMenuRelationRole", "UpdateName", c => c.String());
            AlterColumn("dbo.tblMenuRelationRole", "CreateName", c => c.String());
            AlterColumn("dbo.tblMenuRelationRole", "MenuName", c => c.String());
            AlterColumn("dbo.tblMenuRelationRole", "MenuId", c => c.String());
            AlterColumn("dbo.tblMenuRelationRole", "RoleName", c => c.String());
            AlterColumn("dbo.tblMenu", "UpdateName", c => c.String());
            AlterColumn("dbo.tblMenu", "CreateName", c => c.String());
            AlterColumn("dbo.tblMenu", "FontClass", c => c.String());
            AlterColumn("dbo.tblMenu", "MenuURL", c => c.String());
            AlterColumn("dbo.tblMenu", "Name", c => c.String());
            DropTable("dbo.tblLog");
        }
    }
}
