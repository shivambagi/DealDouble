using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class AuctionService
    {
        

        public Auction GetAuction(int id)
        {
            DealDoubleContext context = new DealDoubleContext();

            return context.Auctions.Find(id);
        }
        public List<Auction> GetAuctions()
        {
            DealDoubleContext context = new DealDoubleContext();

            return context.Auctions.ToList();
        }
        public void SaveAuction(Auction auction)
        {
            DealDoubleContext context = new DealDoubleContext();

            context.Auctions.Add(auction);
            context.SaveChanges();

        }

        public void UpdateAuction(Auction auction)
        {
            //DealDoubleContext context = new DealDoubleContext();
            //var original = context.Auctions.Find(auction.ID);
            //original.Title = auction.Title;
            //original.Description = auction.Description;
            //original.ActualAmount = auction.ActualAmount;
            //original.StartingTime = auction.StartingTime;
            //original.EndTime = auction.EndTime;
            //context.SaveChanges();

            using (DealDoubleContext context = new DealDoubleContext())
            {
                context.Entry(auction).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void DeleteAuction(Auction auction)
        {
            //DealDoubleContext context = new DealDoubleContext();
            //context.Auctions.Remove(auction);
            //context.SaveChanges();

            using (DealDoubleContext context = new DealDoubleContext())
            {
                context.Entry(auction).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

        }
    }
}
