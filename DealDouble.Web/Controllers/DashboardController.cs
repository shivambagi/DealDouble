using DealDouble.Services;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class DashboardController : Controller
    {
        AuctionService aucservice = new AuctionService();
        BidsService bidservice = new BidsService();
        // GET: Dashboard
        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            model.UsersCount = DealDoubleUserManager.GetUsersCount();
            model.AuctionsCount = aucservice.GetAuctionsCount();
            model.BidsCount = bidservice.GetBidsCount();

            return View(model);
        }
    }
}