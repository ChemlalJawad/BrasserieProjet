using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Beers;
using Brasserie.Service.Beers.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace UnitTesting
{
    public class BeerUnitTests
    {

        private Mock<IBrewerRepository> brewerRepositoryMock;
        private Mock<IBeerRepository> beerRepositoryMock;
        private BeerService beerService;

        [Fact]
        public void AddBeer_CommandIsNull_ThrowException()
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            brewerRepositoryMock = new Mock<IBrewerRepository>();
            beerService = new BeerService(brewerRepositoryMock.Object, beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = null;
            try
            {
                Beer actual = beerService.CreateBeer(createBeerCmd);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Command can't be null", e.Message);
            }
        }

        [Fact]
        public void AddBeer_CommandNameIsNull_ThrowException()
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            brewerRepositoryMock = new Mock<IBrewerRepository>();
            beerService = new BeerService(brewerRepositoryMock.Object, beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { AlcoholPercentage = 10, Price = 15, BrewerId = 1 };
            try
            {
                Beer actual = beerService.CreateBeer(createBeerCmd);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Name of beer does not exist", e.Message);
            }
        }

        [Fact]
        public void AddBeer_CommandAmountIsNull_ThrowException()
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            brewerRepositoryMock = new Mock<IBrewerRepository>();
            beerService = new BeerService(brewerRepositoryMock.Object, beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { Name = "Jaj", AlcoholPercentage = 10, Price = -15.00, BrewerId = 1 };
            try
            {
                Beer actual = beerService.CreateBeer(createBeerCmd);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Beer can't be free, Add a good amount", e.Message);
            }
        }

        [Fact]
        public void AddBeer_CommandBrewerIsNull_ThrowException()
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            brewerRepositoryMock = new Mock<IBrewerRepository>();
            beerService = new BeerService(brewerRepositoryMock.Object, beerRepositoryMock.Object);

            List<Brewer> brewers = new List<Brewer>() {
                 new Brewer() { Id = 1, Name = "Le chef" },
                 new Brewer() { Id = 2, Name = "Jade" }
            };
            List<Beer> beers = new List<Beer>()
            {
                new Beer()
                { Id = 1,
                    Name = "Ma binouze",
                    Price = 10,
                    AlcoholPercentage = 7,
                    Brewer = brewers[0],
                    WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 1, Stock = 30 }
                    }
                },
                new Beer()
                { Id = 2,
                    Name = "Ma Chouffe",
                    Price = 17,
                    AlcoholPercentage = 10,
                    Brewer = brewers[1],
                    WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 1, Stock = 30 }
                    }
                }
            };

            beerRepositoryMock.Setup(e => e.GetAll()).Returns(beers);
            brewerRepositoryMock.Setup(e => e.FindById(It.IsAny<int>())).Returns(brewers[0]);

            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { Name = "Jaj", AlcoholPercentage = 10, Price = 15, BrewerId = 4 };
            try
            {
                Beer actual = beerService.CreateBeer(createBeerCmd);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Brewer does not exist", e.Message);
            }
        }

        [Fact]
        public void AddBeer_Ok()
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            brewerRepositoryMock = new Mock<IBrewerRepository>();
            beerService = new BeerService(brewerRepositoryMock.Object, beerRepositoryMock.Object);


            List<Brewer> brewers = new List<Brewer>() {
                 new Brewer() { Id = 1, Name = "Le chef" },
                 new Brewer() { Id = 2, Name = "Jade" }
            };
            List<Beer> beers = new List<Beer>()
            {
                new Beer()
                { Id = 1,
                    Name = "Ma binouze",
                    Price = 10,
                    AlcoholPercentage = 7,
                    Brewer = brewers[0],
                    WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 1, Stock = 30 }
                    }
                },
                new Beer()
                { Id = 2,
                    Name = "Ma Chouffe",
                    Price = 17,
                    AlcoholPercentage = 10,
                    Brewer = brewers[1],
                    WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 1, Stock = 30 }
                    }
                }
            };
            Beer expect = new Beer() { Name = "Jaja", AlcoholPercentage = 10, Price = 15, Brewer = brewers[0] };
            beerRepositoryMock.Setup(e => e.GetAll()).Returns(beers);
            brewerRepositoryMock.Setup(e => e.FindById(It.IsAny<int>())).Returns((brewers[0]));
            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { Name = "Jaja", AlcoholPercentage = 10, Price = 15, BrewerId = 1 };
            Beer actual = beerService.CreateBeer(createBeerCmd);

            Assert.AreEqual(expect.Name, actual.Name);
            Assert.AreEqual(expect.Price, actual.Price);
            Assert.AreEqual(expect.Brewer, actual.Brewer);
        }
    }
}
