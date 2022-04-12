using DealDouble.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class CommentsService
    {
        DealDoubleContext context = new DealDoubleContext();
        public int? GetAverageRate(int entityId, int auctionId)
        {
            var comments = context.Comments.Where(c => c.EntityId == entityId && c.RecordId == auctionId);

            if (comments != null && comments.Count() > 0)
            {
                var rates = comments.Select(c => c.Rating);

                var multipliedRatesCount = (rates.Where(r => r == 5).Count() * 5) +
                        (rates.Where(r => r == 4).Count() * 4) +
                        (rates.Where(r => r == 3).Count() * 3) +
                        (rates.Where(r => r == 2).Count() * 2) +
                        (rates.Where(r => r == 1).Count() * 1);

                decimal average = decimal.Divide(multipliedRatesCount, rates.Count());

                return (Int32)Math.Round(average, MidpointRounding.AwayFromZero);
            }

            return null;
        }
    }
}
