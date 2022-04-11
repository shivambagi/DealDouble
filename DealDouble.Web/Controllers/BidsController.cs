using DealDouble.Entities;
using DealDouble.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class BidsController : Controller
    {
        // GET: Bids
        [HttpPost]
        public ActionResult Bid(int auctionId)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            BidsService bidService = new BidsService();

            if (User.Identity.IsAuthenticated)
            {
                Bid bid = new Bid();

                bid.AuctionID = auctionId;
                bid.UserID = User.Identity.GetUserId();
                bid.Timestamp = DateTime.Now;
                bid.BidAmount = 10;

                var bidResult = bidService.AddBid(bid);

                if (bidResult)
                    result.Data = new { success = true };
                else
                    result.Data = new { success = false, message = "Unable to add bid." };
            }
            else
            {
                result.Data = new { success = false, message = "You have to login first." };
            }

            return result;
        }
    }
}