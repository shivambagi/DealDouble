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
        public ActionResult Create(CreateAuctionViewModel model)
        {
            Auction auction = new Auction();
            auction.Title = model.Title;
            auction.Description = model.Description;
            auction.ActualPrice = model.ActualPrice;
            auction.StartingTime = model.StartingTime;
            auction.EndingTime = model.EndingTime;

            var pictureIDs = model.AuctionPictures.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)
                            .Select(ID => int.Parse(ID)).ToList();

            auction.AuctionPictures = new List<AuctionPicture>();
            auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }));

            #region Same functionality for adding auction pictures
            /*foreach (var picId in pictureIds)
            {
                var auctionPicture = new AuctionPicture();
                auctionPicture.PictureId = picId;
                newAuction.AuctionPictures.Add(auctionPicture);
            } */
            #endregion

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