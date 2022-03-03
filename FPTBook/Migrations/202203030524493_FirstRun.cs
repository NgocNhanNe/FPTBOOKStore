namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Address_Delivery", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Phone_Delivery", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Phone_Delivery");
            DropColumn("dbo.Orders", "Address_Delivery");
        }
    }
}
