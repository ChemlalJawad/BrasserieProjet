using Brasserie.Core.Domains;
using Brasserie.Data;
using Brasserie.Service.Beers;
using Brasserie.Service.Beers.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = NUnit.Framework.Assert;
using Brasserie.Service.Brewers.Services;
using Brasserie.Data.Exceptions;

namespace UnitTesting
{
    public class BeerUnitTests : ServiceContext
    {
        public BeerUnitTests() : base(
            new DbContextOptionsBuilder<BrasserieContext>()
                .UseInMemoryDatabase("Test")
                .Options)
        {
        }

        [Fact]
        public void AddBeer_CommandIsNull_ThrowException()
        {
            using(var context = new BrasserieContext(ContextOptions))
            {
                var service = new BeerService(context);

                Action action = () => service.CreateBeer(null);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage("Command can't be null");
            }

        }
        
        [Fact]
        public void AddBeer_CommandNameIsNull_ThrowException()
        {
            using (var context = new BrasserieContext(ContextOptions))
            {
                var service = new BeerService(context);
                CreateBeerCommand createBeer = new CreateBeerCommand()
                {
                    Price = 10,
                    AlcoholPercentage = 10,
                    BrewerId = new Brewer() { Id = 1 }.Id
                };

                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage("Name of beer does not exist");
            }
          
        }

        [Fact]
        public void AddBeer_CommandAmountEqualZero_ThrowException()
        {
            using (var context = new BrasserieContext(ContextOptions))
            {
                var service = new BeerService(context);
                CreateBeerCommand createBeer = new CreateBeerCommand()
                {
                    Name = "Jaja",
                    Price = 0.00,
                    AlcoholPercentage = 17.00,
                    BrewerId = new Brewer() { Id = 4 }.Id
                };

                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage("Beer can't be free, Add a good amount");
            }
        }

        [Fact]
        public void AddBeer_CommandAmountIsNegative_ThrowException()
        {
            using (var context = new BrasserieContext(ContextOptions))
            {
                var service = new BeerService(context);
                CreateBeerCommand createBeer = new CreateBeerCommand()
                {
                    Name = "Jaja",
                    Price = -10.00,
                    AlcoholPercentage = 17.00,
                    BrewerId = new Brewer() { Id = 4 }.Id
                };
                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage("Beer can't be free, Add a good amount");

            }
        }
             
        [Fact]
        public void AddBeer_BrewerNotExist_ThrowException()
        {
            using (var context = new BrasserieContext(ContextOptions))
            {
                var service = new BeerService(context);
                CreateBeerCommand createBeer = new CreateBeerCommand()
                {
                    Name = "Xavier",
                    Price = 10.00,
                    AlcoholPercentage = 1.00,
                    BrewerId = 40
                    
                };
              
                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<NotFindObjectException>().WithMessage("Brewer does not exist");
            }
        }
             
        [Fact]
        public void AddBeer_Ok()
        {
            using (var context = new BrasserieContext(ContextOptions))
            {
                var service = new BeerService(context);
                CreateBeerCommand createBeer = new CreateBeerCommand()
                {
                    Name = "Xavier",
                    Price = 10.00,
                    AlcoholPercentage = 1.00,
                    BrewerId = 1
                    
                };

                var beer = service.CreateBeer(createBeer);

                beer.Name.Should().Be("Xavier");
                beer.Price.Should().Be(10);
                beer.Brewer.Name.Should().Be("Abbaye de Leffe");
            }
        }
    }
}
