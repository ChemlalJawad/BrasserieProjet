using Brasserie.Core.Domains;
using Brasserie.Data;
using Brasserie.Data.Repositories;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Beers.Services;
using Microsoft.EntityFrameworkCore;

using Moq;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class BeerUnitTest
    {
        public List<Beer> loadData ()
        {
            var brewer1 = new Brewer() { Id = 1, Name = "Brewer 1" };
            var brewer2 = new Brewer() { Id = 2, Name = "Brewer 2" };

            var beer1 = new Beer() { Id = 1, Name = "Jawad", AlcoholPercentage = 1.11, Price = 100.00, Brewer = brewer1};
            var beer2 = new Beer() { Id = 2, Name = "Rafel", AlcoholPercentage = 3.1, Price = 100.00 , Brewer = brewer2};

          /*  var wholesaler = new Wholesaler() { Id = 1, Name = "Le grossiste"};

            var wholesalerbeer1 = new WholesalerBeer() 
            { 
                Beer = beer1, 
                BeerId = beer1.Id,
                Wholesaler = wholesaler,
                WholesalerId = wholesaler.Id, 
                Stock = 100
            };
            
            var wholesalerbeer2 = new WholesalerBeer() 
            { 
                Beer = beer2, 
                BeerId = beer2.Id,
                Wholesaler = wholesaler,
                WholesalerId = wholesaler.Id, 
                Stock = 70
            };


            wholesaler.WholesalerBeers.Add(wholesalerbeer1);
            wholesaler.WholesalerBeers.Add(wholesalerbeer2);
          */
            //beer1.WholesalerBeers.Add(wholesalerbeer1);
            brewer1.Beers.Add(beer1);
            //beer2.WholesalerBeers.Add(wholesalerbeer2);
            brewer2.Beers.Add(beer2);


            var result = new List<Beer>(){ beer1, beer2 };

            return result;
        }
        

        [Fact]
        public void BeerTestGood()
        {
            
            var mockInMemory = loadData();
           
            
            var context = new Mock<BrasserieContext>();
            var dbSetMock = new Mock<DbSet<Beer>>();

            context.Setup(x => x.Set<Beer>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<Beer>())).Callback<Beer>(s => mockInMemory.Add(s));

            
            var repository = new BeerRepository(context.Object);
            var findBeer = repository.FindById(1);

            Assert.Equal(1, findBeer.Id);
        } 
        [Fact]
        public void BeerTest()
        {
            
            var mockInMemory = loadData().First();
            
            
            var mockRepo = new Mock<IBeerRepository>();
            var mockRepo2 = new Mock<IBrewerRepository>();

            mockRepo.Setup(m => m.FindById(mockInMemory.Id)).Returns(mockInMemory);
                        
            var myService = new BeerService(mockRepo,mockRepo2);
            myService.FindById(1);
            var findBeer = myService.FindById(1);

            Assert.Equal(1, findBeer.Id);
        }
    }
}
