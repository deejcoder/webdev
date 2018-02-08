namespace Assignment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Desc = c.String(),
                        Price = c.Int(nullable: false),
                        CID = c.Int(),
                    })
                .PrimaryKey(t => t.PID)
                .ForeignKey("dbo.Categories", t => t.CID)
                .Index(t => t.CID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CID" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
