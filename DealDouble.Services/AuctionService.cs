﻿using DealDouble.Data;
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
        DealDoubleContext context = new DealDoubleContext();

        public Auction GetAuction(int id)
        {
            return context.Auctions.Find(id);
        }
        public List<Auction> GetAuctions()
        {
            return context.Auctions.ToList();
        }

        public List<Auction> GetPromotedAuctions()
        {
            return context.Auctions.Take(4).ToList();
        }
        public void SaveAuction(Auction auction)
        {
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

        public void DeleteAuction(int id)
        {
            //DealDoubleContext context = new DealDoubleContext();
            //context.Auctions.Remove(auction);
            //context.SaveChanges();

            var auction = context.Auctions.Find(id);
            context.Auctions.Remove(auction);
            context.SaveChanges();

            //using (DealDoubleContext context = new DealDoubleContext())
            //{
            //    context.Entry(auction).State = System.Data.Entity.EntityState.Deleted;
            //    context.SaveChanges();
            //}

        }

        //get auctions by filtering them using search, category, and page no
        public List<Auction> FilterAuctions(int? categoryId, string searchTerm, int? pageNo, int pageSize)
        {
            var context = new DealDoubleContext();

            //var auction = context.Auctions.Include(x => x.Category).AsQueryable();
            var auction = context.Auctions.Include("Category").AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                auction = auction.Where(x => x.CategoryID == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                auction = auction.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            pageNo = pageNo ?? 1;
            int skipCount = pageSize * (pageNo.Value - 1);

            return auction.OrderByDescending(a => a.Id).Skip(skipCount).Take(pageSize).ToList();
        }

        public int GetAuctionsCount()
        {
            var context = new DealDoubleContext();

            return context.Auctions.Count();
        }
    }
}
