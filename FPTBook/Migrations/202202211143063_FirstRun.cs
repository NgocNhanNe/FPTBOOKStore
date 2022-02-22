namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Book_ID = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        Cat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Book_ID)
                .ForeignKey("dbo.Categories", t => t.Cat_ID, cascadeDelete: true)
                .Index(t => t.Cat_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Cat_ID = c.Int(nullable: false, identity: true),
                        CatName = c.String(nullable: false),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Cat_ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Book_ID = c.Int(nullable: false),
                        Order_ID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_ID, t.Order_ID })
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID, cascadeDelete: true)
                .Index(t => t.Book_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_ID = c.Int(nullable: false, identity: true),
                        Order_Date = c.DateTime(nullable: false),
                        total = c.Int(nullable: false),
                        Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Order_ID)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 200),
                        Telephone = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        state = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Username", "dbo.Users");
            DropForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.Books", "Cat_ID", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "Username" });
            DropIndex("dbo.OrderDetails", new[] { "Order_ID" });
            DropIndex("dbo.OrderDetails", new[] { "Book_ID" });
            DropIndex("dbo.Books", new[] { "Cat_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
