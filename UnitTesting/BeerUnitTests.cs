using Brasserie.Core.Domains;
using Brasserie.Core.Enums;
using Brasserie.Data;
using Brasserie.Data.Exceptions;
using Brasserie.Service.Beers;
using Brasserie.Service.Beers.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

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
                action.Should().ThrowExactly<HttpBodyException>().WithMessage(ExceptionMessage.COMMAND_IS_NULL);
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
                    BrewerId = 1
                };

                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage(ExceptionMessage.NAME_BEER_NOT_EXIST);
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
                    BrewerId = 4
                };

                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage(ExceptionMessage.PRICE_NULL_OR_NEGATIVE);
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
                    BrewerId = 1
                };
                Action action = () => service.CreateBeer(createBeer);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage(ExceptionMessage.PRICE_NULL_OR_NEGATIVE);

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
                action.Should().ThrowExactly<NotFindObjectException>().WithMessage(ExceptionMessage.BREWER_NOT_EXIST);
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
