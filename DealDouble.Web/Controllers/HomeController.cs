using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class HomeController : Controller
    {
        AuctionService aucservice = new AuctionService();
        public ActionResult Index()
        {
            AuctionsViewModel avm = new AuctionsViewModel();
            avm.PageTitle = "Home";
            avm.PageDescription = "This is Home page";
            avm.AllAuctions = aucservice.GetAuctions();
            avm.PromotedAuctions = aucservice.GetPromotedAuctions();
            return View(avm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}