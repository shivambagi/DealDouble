using DealDouble.Data;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class SharedService
    {
        DealDoubleContext context = new DealDoubleContext();

        public int SavePicture(Picture picture)
        {
            context.Pictures.Add(picture);
            context.SaveChanges();

            return picture.Id;
        }
    }
}
