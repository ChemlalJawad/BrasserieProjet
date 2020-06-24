using Brasserie.Core.Domains;
using Brasserie.Data.Repositories;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Wholesalers;
using Brasserie.Service.Wholesalers.Services;
using Brasserie.Service.Wholesalers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTesting
{
    public class WholesalerUnitTests
    {
        [Fact]
        public void SellBeer_IsNull_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object,beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = null;
            try
            { 
                wholesalerService.SellNewBeer(sellBeerCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Command can't be null", e.Message);
            }       
        }

         [Fact]
        public void SellBeer_BeerIdIsNull_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object,beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = new SellBeerCommand() {WholesalerId = 1 , Stock = 10};
            try
            { 
                wholesalerService.SellNewBeer(sellBeerCommand);
            }
            catch (Exception e)
            {
                Assert.Equal("Beer does not exist", e.Message);
            }       
        } 
        [Fact]
        public void SellBeer_WholesalerIdIsNull_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object,beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = new SellBeerCommand() {BeerId = 1 , Stock = 10};
            try
            { 
                wholesalerService.SellNewBeer(sellBeerCommand);
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
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object,beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = new SellBeerCommand() {BeerId = 1 ,WholesalerId = 2, Stock = -10};
            try
            { 
                wholesalerService.SellNewBeer(sellBeerCommand);
            }

            catch (Exception e)
            {
                Assert.Equal("You can't add a negative stock", e.Message);
            }       
        }

        [Fact]
        public void SellBeer_Duplicate_ThrowException()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            SellBeerCommand sellBeerCommand = new SellBeerCommand() { BeerId = 1, WholesalerId = 2, Stock = 10 };
            List<WholesalerBeer> wholesalerbeers = new List<WholesalerBeer>()
            {
                new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 2, Stock = 10 },
                new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 3, Stock = 10 }
            };

            wholesalerRepositoryMock.Setup(e => e.GetAll()).Returns(wholesalerbeers);
            try 
            { 
                wholesalerService.SellNewBeer(sellBeerCommand); 

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

            UpdateStockCommand updateBeerCommand = new UpdateStockCommand() { BeerId = 1, WholesalerId = 2, Stock = -10 };
            List<WholesalerBeer> wholesalerbeers = new List<WholesalerBeer>()
            {
                new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 2, Stock = 10 },
                new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 3, Stock = 10 }
            };
            wholesalerRepositoryMock.Setup(e => e.GetAll()).Returns(wholesalerbeers);
            try
            {
                wholesalerService.UpdateStock(updateBeerCommand);
            }

            catch (Exception e)
            {
                Assert.Equal("You can't add a negative stock", e.Message);
            }
        }
        [Fact]
        public void UpdateStock_Stock_Ok()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            UpdateStockCommand updateBeerCommand = new UpdateStockCommand() { BeerId = 1, WholesalerId = 2, Stock = 9 };
            List<WholesalerBeer> wholesalerbeers = new List<WholesalerBeer>()
            {
                new WholesalerBeer(){ BeerId = 1 ,WholesalerId = 2, Stock = 10 },
                new WholesalerBeer(){ BeerId = 2 ,WholesalerId = 3, Stock = 10 }
            };
            wholesalerRepositoryMock.Setup(e => e.GetAll()).Returns(wholesalerbeers);
            var result = wholesalerService.UpdateStock(updateBeerCommand);

            Assert.Equal(updateBeerCommand.Stock, result.Stock);
            wholesalerRepositoryMock.Verify(e => e.UpdateStock(result));       
        }
              
        [Fact]
        public void GetQuotation_IsNull()
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
        public void GetQuotation_WholesalerNotExist()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);               
            
            QuotationCommand quotationCommand = new QuotationCommand()
            {              
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
        public void GetQuotation_DuplicateOrder()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

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
        public void GetQuotation_BeerNotExist()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

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
        public void GetQuotation_StockNotEnough()
        {
            wholesalerRepositoryMock = new Mock<IWholesalerRepository>();
            beerRepositoryMock = new Mock<IBeerRepository>();
            wholesalerService = new WholesalerService(wholesalerRepositoryMock.Object, beerRepositoryMock.Object);

            QuotationCommand quotationCommand = new QuotationCommand()
            {  
                WholesalerId = 1,          
                TotalPrice = 100,
                Items = new List<ItemCommand>
                {
                    new ItemCommand()
                    { 
                        BeerId= 7,
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

        private Mock<IWholesalerRepository> wholesalerRepositoryMock; 
        private Mock<IBeerRepository> beerRepositoryMock;
        private WholesalerService wholesalerService;
    }
}
