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
            if(Request.IsAjaxRequest())
            {
                return PartialView(alvm);
            }
            else
            {
                return View(alvm);
            }
            
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
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var auction = aser.GetAuction(id);
            return View(auction);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            aser.DeleteAuction(auction);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var auction = aser.GetAuction(id);
            return View(auction);
        }
    }
}