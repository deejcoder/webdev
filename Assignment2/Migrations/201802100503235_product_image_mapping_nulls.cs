namespace Assignment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_image_mapping_nulls : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImageMappings", "PID", "dbo.Products");
            DropIndex("dbo.ProductImageMappings", new[] { "PID" });
            AlterColumn("dbo.ProductImageMappings", "PID", c => c.Int());
            AlterColumn("dbo.ProductImageMappings", "PImageID", c => c.Int());
            CreateIndex("dbo.ProductImageMappings", "PID");
            AddForeignKey("dbo.ProductImageMappings", "PID", "dbo.Products", "PID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImageMappings", "PID", "dbo.Products");
            DropIndex("dbo.ProductImageMappings", new[] { "PID" });
            AlterColumn("dbo.ProductImageMappings", "PImageID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductImageMappings", "PID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductImageMappings", "PID");
            AddForeignKey("dbo.ProductImageMappings", "PID", "dbo.Products", "PID", cascadeDelete: true);
        }
    }
}
