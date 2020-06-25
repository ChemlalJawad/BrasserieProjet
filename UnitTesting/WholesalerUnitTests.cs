using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Wholesalers;
using Brasserie.Service.Wholesalers.Services;
using Brasserie.Service.Wholesalers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTesting
{
    public class WholesalerUnitTests
    {
        private Mock<IWholesalerRepository> wholesalerRepositoryMock;
        private Mock<IBeerRepository> beerRepositoryMock;
        private WholesalerService wholesalerService;

        [Fact]
        public void AddBeer_CommandIsNull_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = null;
            try
            {
                wholesalerService.AddNewBeerToWholesaler(sellBeerCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Command can't be null", e.Message);
            }
        }

        [Fact]
        public void AddBeer_BeerNotExist_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();

            SellBeerCommand sellBeerCommand = new SellBeerCommand() { BeerId = 10 ,WholesalerId = 1, Stock = 10 };
            try
            {
                wholesalerService.AddNewBeerToWholesaler(sellBeerCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Beer does not exist", e.Message);
            }
        }
        [Fact]
        public void AddBeer_WholesalerNotExist_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();

            SellBeerCommand sellBeerCommand = new SellBeerCommand() { BeerId = 1, WholesalerId = 10, Stock = 10 };
            try
            {
                wholesalerService.AddNewBeerToWholesaler(sellBeerCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Wholesaler does not exist", e.Message);
            }
        }

        [Fact]
        public void SellBeer_StockIsNegative_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = new SellBeerCommand() { BeerId = 1, WholesalerId = 2, Stock = -10 };
            try
            {
                wholesalerService.AddNewBeerToWholesaler(sellBeerCommand);
            }

            catch (Exception e)
            {
                Assert.Equal("You can't add a negative stock", e.Message);
            }
        }

        [Fact]
        public void AddBeer_DuplicateBeer_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);           
            LoadMockData();
            
            SellBeerCommand sellBeerCommand = new SellBeerCommand() { BeerId = 1, WholesalerId = 1, Stock = 10 };           
                      
            try
            {
                wholesalerService.AddNewBeerToWholesaler(sellBeerCommand);

            }
            catch (Exception e)
            {
                Assert.Equal("Wholesaler already sell this beer", e.Message);
            }
        }

        [Fact]
        public void UpdateStock_StockIsNegative_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();
            UpdateStockCommand updateBeerCommand = new UpdateStockCommand() { BeerId = 1, WholesalerId = 1, Stock = -10 };
            
            try
            {
                wholesalerService.UpdateWholesalerBeer(updateBeerCommand);
            }

            catch (Exception e)
            {
                Assert.Equal("You can't add a negative stock", e.Message);
            }
        }

        [Fact]
        public void UpdateStock_Stock_IsOk()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();
            
            UpdateStockCommand updateBeerCommand = new UpdateStockCommand() { BeerId = 1, WholesalerId = 1, Stock = 9 }; 
            var result = wholesalerService.UpdateWholesalerBeer(updateBeerCommand);
            
            Assert.Equal(updateBeerCommand.Stock, result.Stock);
        }

        [Fact]
        public void GetQuotation_CommandItemIsNull_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                TotalPrice = 100,
                Items = null
            };

            try
            {
                wholesalerService.GetQuotation(quotationCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Command can't be null !", e.Message);
            }
        }

        [Fact]
        public void GetQuotation_WholesalerNotExist_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 2,
                TotalPrice = 100,
                Items = new List<ItemCommand>
                {
                    new ItemCommand() { BeerId= 1, Quantity = 10},
                    new ItemCommand() { BeerId= 2, Quantity = 5}
                }
            };

            try
            {
                wholesalerService.GetQuotation(quotationCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Wholesaler does not exist!", e.Message);
            }
        }
        [Fact]
        public void GetQuotation_CheckDuplicateOrder_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                TotalPrice = 100,
                Items = new List<ItemCommand>
                {
                    new ItemCommand() { BeerId= 7, Quantity = 10},
                    new ItemCommand() { BeerId= 7, Quantity = 10}
                }
            };

            try
            {
                wholesalerService.GetQuotation(quotationCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("You can't have duplicates items in your Order", e.Message);
            }
        }

        [Fact]
        public void GetQuotation_BeerNotExist_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                TotalPrice = 100,
                Items = new List<ItemCommand>
                {
                    new ItemCommand() { BeerId= 7, Quantity = 10}
                }
            };

            try
            {
                wholesalerService.GetQuotation(quotationCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Beer does not exist", e.Message);
            }
        }

        [Fact]
        public void GetQuotation_StockNotEnough_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            LoadMockData();

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                TotalPrice = 100,
                Items = new List<ItemCommand>
                {
                    new ItemCommand()
                    {
                        BeerId= 1,
                        Quantity = 40
                    }
                }
            };

            try
            {
                wholesalerService.GetQuotation(quotationCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("You don't have enough stocks!", e.Message);
            }
        }

        [Fact]
        public void GetQuotation_Discount10_IsOk()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            var expect = 180;
            LoadMockData();

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                Items = new List<ItemCommand>
                {
                    new ItemCommand()
                    {
                        BeerId= 1,
                        Quantity = 20
                    }
                }
            };

            var actual = wholesalerService.GetQuotation(quotationCommand);
            Assert.Equal(expect, actual);

        }
        [Fact]
        public void GetQuotation_NoDiscount_IsOk()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            var expect = 90;
            LoadMockData();

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                Items = new List<ItemCommand>
                {
                    new ItemCommand()
                    {
                        BeerId= 1,
                        Quantity = 9
                    }
                }
            };

            var actual = wholesalerService.GetQuotation(quotationCommand);
            Assert.Equal(expect, actual);

        }

        [Fact]
        public void GetQuotation_Discount20_IsOk()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);
            var expect = 200;
            LoadMockData();

            QuotationCommand quotationCommand = new QuotationCommand()
            {
                WholesalerId = 1,
                Items = new List<ItemCommand>
                {
                    new ItemCommand()
                    {
                        BeerId= 1,
                        Quantity = 25
                    }
                }
            };

            var actual = wholesalerService.GetQuotation(quotationCommand);
            Assert.Equal(expect, actual);

        }

        internal void LoadMockData()
        {
            List<Wholesaler> wholesalers = new List<Wholesaler>()
            {
              new Wholesaler()
              {  
                Id = 1,
                Name = "Jaja",
                WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 1, Stock = 30 },
                        new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 1, Stock = 30 },
                    }

              }
            };

            List<Beer> beers = new List<Beer>()
            {
                new Beer()
                {   
                    Id = 1,
                    Name = "Ma binouze",
                    Price = 10,
                    AlcoholPercentage = 7,
                    WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 1, Stock = 30 }
                    }
                },
                new Beer()
                { 
                    Id = 2,
                    Name = "Ma Chouffe",
                    Price = 17,
                    AlcoholPercentage = 10,
                    WholesalerBeers = new List<WholesalerBeer>()
                    {
                        new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 1, Stock = 30 }
                    }
                }
            };

            beerRepositoryMock.Setup(e => e.GetAll()).Returns(beers);
            wholesalerRepositoryMock.Setup(e => e.FindById(It.IsAny<int>())).Returns((int arg1) => wholesalers.Where(ws => ws.Id == arg1).SingleOrDefault());
            beerRepositoryMock.Setup(e => e.FindById(It.IsAny<int>())).Returns((int arg1) => beers.Where(b => b.Id == arg1).SingleOrDefault());
        }
    }
}
