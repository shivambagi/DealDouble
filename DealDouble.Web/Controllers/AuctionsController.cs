using DealDouble.Entities;
using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            AuctionService aser = new AuctionService();
            var listauction = aser.GetAuctions();
            return View(listauction);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            AuctionService aser = new AuctionService();
            aser.SaveAuction(auction);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AuctionService aser = new AuctionService();
            var auction = aser.GetAuction(id);
            return View(auction);
        }

        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            AuctionService aser = new AuctionService();
            aser.UpdateAuction(auction);
            return View(auction);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AuctionService aser = new AuctionService();
            var auction = aser.GetAuction(id);
            return View(auction);
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            AuctionService aser = new AuctionService();
            aser.DeleteAuction(auction);
            return RedirectToAction("Index");
        }
    }
}