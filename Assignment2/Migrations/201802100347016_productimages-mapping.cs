namespace Assignment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productimagesmapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImageMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageNumber = c.Int(nullable: false),
                        PID = c.Int(nullable: false),
                        PImageID = c.Int(nullable: false),
                        ProductImage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.PID, cascadeDelete: true)
                .ForeignKey("dbo.ProductImages", t => t.ProductImage_ID)
                .Index(t => t.PID)
                .Index(t => t.ProductImage_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImageMappings", "ProductImage_ID", "dbo.ProductImages");
            DropForeignKey("dbo.ProductImageMappings", "PID", "dbo.Products");
            DropIndex("dbo.ProductImageMappings", new[] { "ProductImage_ID" });
            DropIndex("dbo.ProductImageMappings", new[] { "PID" });
            DropTable("dbo.ProductImageMappings");
        }
    }
}
