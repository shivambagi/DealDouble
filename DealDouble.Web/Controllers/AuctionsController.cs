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
        CategoryService cser = new CategoryService();
        [HttpGet]
        public ActionResult Index(int? categoryId, string searchTerm, int? pageNo)
        {
            AuctionListingViewModel alvm = new AuctionListingViewModel();
            alvm.PageTitle = "Auction Index";
            alvm.PageDescription = "This is Auction Index page";
            
            alvm.CategoryId = categoryId;
            alvm.SearchTerm = searchTerm;
            alvm.PageNo = pageNo;

            alvm.Categories = cser.GetCategories();

            return View(alvm);


        }

        public PartialViewResult Listing(int? categoryId, string searchTerm, int? pageNo)
        {
            var pageSize = 4;

            var auctionsModel = new AuctionListingViewModel();


            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            //auctionsModel.Auctions = aser.GetAuctions();

            auctionsModel.Auctions = aser.FilterAuctions(categoryId, searchTerm, pageNo, pageSize);

            int totalAuctions;

            if (categoryId != null || searchTerm != null)
            {
                totalAuctions = auctionsModel.Auctions.Count();  //aser.GetAuctionsCount();
            }
            else
            {
                totalAuctions = aser.GetAuctionsCount();
            }

            auctionsModel.Pager = new Pager(totalAuctions, pageNo, pageSize);

            return PartialView(auctionsModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();
            model.Categories = cser.GetCategories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel model)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                Auction auction = new Auction();
                auction.Title = model.Title;
                auction.CategoryID = model.CategoryID;
                auction.Description = model.Description;
                auction.ActualPrice = model.ActualPrice;
                auction.StartingTime = model.StartingTime;
                auction.EndingTime = model.EndingTime;

                if (!String.IsNullOrEmpty(model.AuctionPictures))
                {

                    var pictureIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(ID => int.Parse(ID)).ToList();

                    auction.AuctionPictures = new List<AuctionPicture>();
                    auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }));
                }

                #region Same functionality for adding auction pictures
                /*foreach (var picId in pictureIds)
                {
                    var auctionPicture = new AuctionPicture();
                    auctionPicture.PictureId = picId;
                    newAuction.AuctionPictures.Add(auctionPicture);
                } */
                #endregion

                aser.SaveAuction(auction);

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid inputs." };
            }

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            CreateAuctionViewModel model = new CreateAuctionViewModel();
            var auction = aser.GetAuction(id);
            model.ID = auction.Id;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.Description = auction.Description;
            model.ActualPrice = auction.ActualPrice;
            model.StartingTime = auction.StartingTime;
            model.EndingTime = auction.EndingTime;
            model.AuctionPicturesList = auction.AuctionPictures;

            model.Categories = cser.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(CreateAuctionViewModel model)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                Auction auction = new Auction();
                auction.Id = model.ID;
                auction.Title = model.Title;
                auction.Description = model.Description;
                auction.ActualPrice = model.ActualPrice;
                auction.StartingTime = model.StartingTime;
                auction.EndingTime = model.EndingTime;
                auction.CategoryID = model.CategoryID;

                //there's a BUG here (update auction pictures)
                //check if we have aictionpictureIds back from form  
                if (!String.IsNullOrEmpty(model.AuctionPictures))
                {
                    var pictureIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(ID => int.Parse(ID)).ToList();

                    auction.AuctionPictures = new List<AuctionPicture>();
                    auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { AuctionID = auction.Id, PictureID = x }));
                }

                aser.UpdateAuction(auction);

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid inputs." };
            }

            return result;
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
            AuctionDetailsViewModel model = new AuctionDetailsViewModel();
            model.Auction = aser.GetAuction(id);
            model.PageTitle = model.Auction.Title + "Details";
            model.PageDescription = model.Auction.Description;
            return View(model);
        }
    }
}