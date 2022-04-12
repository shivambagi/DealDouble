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
        SharedService sharedservice = new SharedService();
        CommentsService commentsService = new CommentsService();
        [HttpGet]
        public ActionResult Index(int? categoryID, string searchTerm, int? pageNo)
        {
            AuctionListingViewModel alvm = new AuctionListingViewModel();
            alvm.PageTitle = "Auction Index";
            alvm.PageDescription = "This is Auction Index page";
            
            alvm.CategoryId = categoryID;
            alvm.SearchTerm = searchTerm;
            alvm.PageNo = pageNo;

            alvm.Categories = cser.GetCategories();

            return View(alvm);


        }

        public PartialViewResult Listing(int? categoryID, string searchTerm, int? pageNo)
        {
            var pageSize = 4;

            var auctionsModel = new AuctionListingViewModel();


            auctionsModel.PageTitle = "Auctions";
            auctionsModel.PageDescription = "Auctions listing page.";

            //auctionsModel.Auctions = aser.GetAuctions();

            auctionsModel.Auctions = aser.FilterAuctions(categoryID, searchTerm, pageNo, pageSize);

            int totalAuctions;
                       
            totalAuctions = aser.GetAuctionsCount(categoryID, searchTerm);

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

                //check if we have auctionPictureIds back from form  
                if (!String.IsNullOrEmpty(model.AuctionPictures))
                {
                    var pictureIds = model.AuctionPictures.Split(',').Select(int.Parse);

                    auction.AuctionPictures = new List<AuctionPicture>(); //empty auctionPicture object before modified
                    auction.AuctionPictures.AddRange(pictureIds.Select(pi => new AuctionPicture()
                    {
                        PictureID = pi,
                        AuctionID = auction.Id //prevent create new auction
                    }).ToList());
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
            model.PageDescription = model.Auction.Description != null ? (model.Auction.Description.Length > 10 ? model.Auction.Description.Substring(0, 10) : model.Auction.Description) : "Auction Details.";

            var latestBidder = model.Auction.Bids.OrderByDescending(b => b.Timestamp).FirstOrDefault();

            model.LatestBidder = latestBidder != null ? latestBidder.User : null;

            model.BidsAmount = model.Auction.ActualPrice + model.Auction.Bids.Sum(x => x.BidAmount);
            model.EntityId = Convert.ToInt32(EntitiesEnum.Auction);

            model.AverageRate = commentsService.GetAverageRate(model.EntityId, id);

            model.Comments = sharedservice.GetComments(model.EntityId, id);


            return View(model);
        }
    }
}