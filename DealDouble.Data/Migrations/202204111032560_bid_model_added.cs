namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bid_model_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BidAmount = c.Double(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AuctionID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.AuctionID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.AuctionID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "AuctionID", "dbo.Auctions");
            DropIndex("dbo.Bids", new[] { "UserID" });
            DropIndex("dbo.Bids", new[] { "AuctionID" });
            DropTable("dbo.Bids");
        }
    }
}
