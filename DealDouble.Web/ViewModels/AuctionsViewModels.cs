using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class AuctionListingViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
        public Pager Pager { get; set; }

        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
        public int? PageNo { get; set; }

        public List<Category> Categories { get; set; }
    }
    public class AuctionsViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Auction> PromotedAuctions { get; set; }

        public int EntityId { get; set; }
    }
    public class AuctionDetailsViewModel : PageViewModel
    {
        public Auction Auction { get; set; }
        public decimal BidsAmount { get; set; }

        public DealDoubleUser LatestBidder { get; set; }

        public int EntityId { get; set; }

        public int? AverageRate { get; set; }

        public List<Comment> Comments { get; set; }
    }

    

    public class CreateAuctionViewModel : PageViewModel
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        [Required, MinLength(4), MaxLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }
        [Range(10, 1000000)]
        public decimal ActualPrice { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        public string AuctionPictures { get; set; }

        public List<Category> Categories { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }
    }
}