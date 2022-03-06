namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetails", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Price", c => c.Int(nullable: false));
        }
    }
}
