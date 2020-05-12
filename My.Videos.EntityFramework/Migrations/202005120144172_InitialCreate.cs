namespace My.Videos.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MenuURL = c.String(),
                        FontClass = c.String(),
                        ParentId = c.Int(nullable: false),
                        CreateName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateName = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblMenuRelationRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        RoleName = c.String(),
                        MenuId = c.String(),
                        MenuName = c.String(),
                        CreateName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateName = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 10),
                        IsDelete = c.Int(nullable: false),
                        CreateName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateName = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        MobilePhone = c.String(maxLength: 50),
                        NickName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 100),
                        CreateName = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateName = c.String(maxLength: 50),
                        UpdateTime = c.DateTime(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblUserRelationMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.String(),
                        MenuName = c.String(),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        CreateName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateName = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblUserRelationMenu");
            DropTable("dbo.tblUser");
            DropTable("dbo.tblRole");
            DropTable("dbo.tblMenuRelationRole");
            DropTable("dbo.tblMenu");
        }
    }
}
