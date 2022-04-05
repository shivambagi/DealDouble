namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Auctions", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Auctions", "CategoryID");
            AddForeignKey("dbo.Auctions", "CategoryID", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "CategoryID" });
            DropColumn("dbo.Auctions", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
