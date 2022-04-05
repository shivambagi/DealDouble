using DealDouble.Entities;
using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionService aser = new AuctionService();
        [HttpGet]
        public ActionResult Index()
        {
            AuctionListingViewModel alvm = new AuctionListingViewModel();
            alvm.PageTitle = "Auction Index";
            alvm.PageDescription = "This is Auction Index page";
            alvm.Auctions = aser.GetAuctions();
            
                return View(alvm);
            
            
        }

        public PartialViewResult Listing()
        {
            var auctionsModel = new AuctionListingViewModel();

            
            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            auctionsModel.Auctions = aser.GetAuctions();

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            aser.SaveAuction(auction);
            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var auction = aser.GetAuction(id);
            return PartialView(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            aser.UpdateAuction(auction);
            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            aser.DeleteAuction(id);
            return RedirectToAction("Listing");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var auction = aser.GetAuction(id);
            return View(auction);
        }
    }
}