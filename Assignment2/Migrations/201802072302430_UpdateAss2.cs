namespace Assignment2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAss2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Products", "Desc", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Desc", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
