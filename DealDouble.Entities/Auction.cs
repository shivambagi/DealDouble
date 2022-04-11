using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Auction : BaseEntity
    {
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        [Required, MinLength(4), MaxLength(150)]
        public string Title { get; set; }
        
        public string Description { get; set; }
        [Required, Range(10, 1000000)]
        public decimal ActualPrice { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        public virtual List<AuctionPicture> AuctionPictures { get; set; }

        public virtual List<Bid> Bids { get; set; }

    }
}
