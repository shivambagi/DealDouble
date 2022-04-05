namespace DealDouble.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAuctionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "ActualPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Auctions", "EndingTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Auctions", "ActualAmount");
            DropColumn("dbo.Auctions", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Auctions", "ActualAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Auctions", "EndingTime");
            DropColumn("dbo.Auctions", "ActualPrice");
        }
    }
}
