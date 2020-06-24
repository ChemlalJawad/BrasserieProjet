using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Beers;
using Brasserie.Service.Beers.Services;
using Brasserie.Service.Brewers.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace UnitTesting
{
    public class BeerUnitTests
    {
        [Fact]
        public void AddBeer_CommandIsNull_ThrowException() 
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            beerService = new BeerService(beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = null;
            try
            {
                Beer actual = beerService.CreateBeer(createBeerCmd);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Null", e.Message);
            }
        } 

        [Fact]
        public void AddBeer_CommandNameIsNull_ThrowException() 
        {
            beerRepositoryMock = new Mock<IBeerRepository>();
            beerService = new BeerService(beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { AlcoholPercentage = 10,Price = 15, BrewerId = 1 };
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
            beerService = new BeerService(beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { Name ="Jaj", AlcoholPercentage = 10, Price = -15.00, BrewerId = 1 };
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
            beerService = new BeerService(beerRepositoryMock.Object);

            CreateBeerCommand createBeerCmd = new CreateBeerCommand() { Name = "Jaj", AlcoholPercentage = 10, Price = 15, BrewerId = 1 };
            try
            {
                Beer actual = beerService.CreateBeer(createBeerCmd);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Brewer does not exist", e.Message);
            }         
        }
        
        private Mock<IBeerRepository> beerRepositoryMock;
        private BeerService beerService;
    }
}
