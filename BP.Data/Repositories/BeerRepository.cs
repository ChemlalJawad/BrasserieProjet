using BP.Core.Domains;
using BP.Data.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.Data.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BrasserieContext _brasserieContext;

        public BeerRepository(BrasserieContext brasserieContext)
        {
            _brasserieContext = brasserieContext;
        }


        public IEnumerable<Beer> GetAll()
        {
            var beers = _brasserieContext.Beers.ToList();

            return beers;
        }

        public void CreateBeer(Beer beer)
        {
            _brasserieContext.Beers.Add(beer);
            _brasserieContext.SaveChanges();
        }

        public Beer FindBeerById(int beerId)
        {
            var beerResult = _brasserieContext.Beers
                .Include(b => b.Brewer)
                    .FirstOrDefault(e => e.Id == beerId);

            return beerResult;
        }

        public void Delete(int beerId)
        {
            _brasserieContext.Beers.Remove(new Beer() { Id = beerId });
            _brasserieContext.SaveChanges();
        }
    }
}
