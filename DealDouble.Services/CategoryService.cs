using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class CategoryService
    {
        DealDoubleContext context = new DealDoubleContext();

        public Category GetCategory(int id)
        {
            return context.Categories.Find(id);
        }
        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public List<Category> GetPromotedCategoriess()
        {
            return context.Categories.Take(4).ToList();
        }
        public void SaveCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();

        }

        public void UpdateCategory(Category category)
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
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void DeleteCategory(int id)
        {
            //DealDoubleContext context = new DealDoubleContext();
            //context.Auctions.Remove(auction);
            //context.SaveChanges();

            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();

            //using (DealDoubleContext context = new DealDoubleContext())
            //{
            //    context.Entry(auction).State = System.Data.Entity.EntityState.Deleted;
            //    context.SaveChanges();
            //}

        }
    }
}
