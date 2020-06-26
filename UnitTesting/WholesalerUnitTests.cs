using Brasserie.Data;
using Brasserie.Data.Exceptions;
using Brasserie.Service.Wholesalers;
using Brasserie.Service.Wholesalers.Services;
using Brasserie.Service.Wholesalers.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTesting
{
    public class WholesalerUnitTests : ServiceContext
    {
        public WholesalerUnitTests() : base(
           new DbContextOptionsBuilder<BrasserieContext>()
               .UseInMemoryDatabase("Test")
               .Options)
        {
        }

        [Fact]
        public void AddBeer_CommandIsNull_ThrowException()
        {
           using (var context = new BrasserieContext(ContextOptions))
            { 
                var wholesalerService = new WholesalerService(context);

                Action action = () => wholesalerService.AddNewBeerToWholesaler(null);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage("Command can't be null");
            }
        }
       

        [Fact]
        public void AddBeer_BeerNotExist_ThrowException()
        {
            SellBeerCommand command = new SellBeerCommand() { BeerId = 25, WholesalerId = 1, Stock = 12 };
            using (var context = new BrasserieContext(ContextOptions))
            {
                var wholesalerService = new WholesalerService(context);

                Action action = () => wholesalerService.AddNewBeerToWholesaler(command);
                action.Should().ThrowExactly<NotFindObjectException>().WithMessage("Beer does not exist");
            }
        }
       
       [Fact]
       public void AddBeer_WholesalerNotExist_ThrowException()
       {
            SellBeerCommand command = new SellBeerCommand() { BeerId = 2, WholesalerId = 7, Stock = 10 };
            using (var context = new BrasserieContext(ContextOptions))
            {
                var wholesalerService = new WholesalerService(context);

                Action action = () => wholesalerService.AddNewBeerToWholesaler(command);
                action.Should().ThrowExactly<NotFindObjectException>().WithMessage("Wholesaler does not exist");
            }
            
       }
       
      [Fact]
      public void AddBeer_StockIsNegative_ThrowException()
      {
          SellBeerCommand command = new SellBeerCommand() { BeerId = 1, WholesalerId = 2, Stock = -10 };
          using (var context = new BrasserieContext(ContextOptions))
          {
                var wholesalerService = new WholesalerService(context);

                Action action = () => wholesalerService.AddNewBeerToWholesaler(command);
                action.Should().ThrowExactly<HttpBodyException>().WithMessage("You can't add a negative stock");
          }
      }
    
      [Fact]
      public void AddBeer_DuplicateBeer_ThrowException()
      {
          SellBeerCommand command = new SellBeerCommand() { BeerId = 1, WholesalerId = 1, Stock = 10 };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              Action action = () => wholesalerService.AddNewBeerToWholesaler(command);
              action.Should().ThrowExactly<DuplicateItemException>().WithMessage("Wholesaler already sell this beer");
          }
      }    

      [Fact]
      public void AddBeer_OK()
      {
          SellBeerCommand command = new SellBeerCommand() { BeerId = 5, WholesalerId = 2, Stock = 1000 };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              var actual = wholesalerService.AddNewBeerToWholesaler(command);

              actual.BeerId.Should().Be(command.BeerId);
              actual.WholesalerId.Should().Be(command.WholesalerId);
              actual.Stock.Should().Be(command.Stock);
            
          }
      }

      [Fact]
      public void UpdateStock_StockIsNegative_ThrowException()
      {
          UpdateStockCommand command = new UpdateStockCommand() { BeerId = 1, WholesalerId = 1, Stock = -10 };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);
                
              Action action = () => wholesalerService.UpdateWholesalerBeer(command);
              action.Should().ThrowExactly<HttpBodyException>().WithMessage("You can't add a negative stock");
          }
      }

      [Fact]
      public void UpdateStock_Stock_IsOk()
      {
          UpdateStockCommand command = new UpdateStockCommand() { BeerId = 1, WholesalerId = 1, Stock = 9 };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              var actual = wholesalerService.UpdateWholesalerBeer(command);
              actual.Stock.Should().Be(9);
          }
      }
        
      [Fact]
      public void GetQuotation_CommandItemIsNull_ThrowException()
      {
          QuotationCommand command = new QuotationCommand()
          {
              WholesalerId = 1,
              TotalPrice = 100,
              Items = null
          };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              Action action = () => wholesalerService.GetQuotation(command);
              action.Should().ThrowExactly<HttpBodyException>().WithMessage("Command can't be null !");
          }            
      }

      [Fact]
      public void GetQuotation_WholesalerNotExist_ThrowException()
      {
          QuotationCommand command = new QuotationCommand()
          {
              WholesalerId = 70,
              TotalPrice = 100,
              Items = new List<ItemCommand>
              {
                  new ItemCommand() { BeerId= 1, Quantity = 10},
                  new ItemCommand() { BeerId= 2, Quantity = 5}
              }
          };

          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);
                
              Action action = () => wholesalerService.GetQuotation(command);
              action.Should().ThrowExactly<NotFindObjectException>().WithMessage("Wholesaler does not exist!");
          }           
      }

      [Fact]
      public void GetQuotation_CheckDuplicateOrder_ThrowException()
      {
          QuotationCommand command = new QuotationCommand()
          {
              WholesalerId = 1,
              TotalPrice = 100,
              Items = new List<ItemCommand>
              {
                  new ItemCommand() { BeerId= 7, Quantity = 10},
                  new ItemCommand() { BeerId= 7, Quantity = 3}
              }
          };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              Action action = () => wholesalerService.GetQuotation(command);
              action.Should().ThrowExactly<DuplicateItemException>().WithMessage("You can't have duplicates items in your Order");
          }
      }

      [Fact]
      public void GetQuotation_BeerNotExist_ThrowException()
      {
          QuotationCommand command = new QuotationCommand()
          {
              WholesalerId = 1,
              TotalPrice = 100,
              Items = new List<ItemCommand>
              {
                  new ItemCommand() { BeerId= 7, Quantity = 10}
              }
          };
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              Action action = () => wholesalerService.GetQuotation(command);
              action.Should().ThrowExactly<NotFindObjectException>().WithMessage("Beer is not sell");
          }
      }

      [Fact]
      public void GetQuotation_StockNotEnough_ThrowException()
      {
          QuotationCommand command = new QuotationCommand()
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
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              Action action = () => wholesalerService.GetQuotation(command);
              action.Should().ThrowExactly<NotEnoughQuantityException>().WithMessage("You don't have enough stocks!");
          }
      }

      [Fact]
      public void GetQuotation_Discount10_IsOk()
      {
          var expect = 39.6;
          QuotationCommand command = new QuotationCommand()
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
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              var actual = wholesalerService.GetQuotation(command);
              actual.Should().Be(expect);
          }
      }

      [Fact]
      public void GetQuotation_NoDiscount_IsOk()
      {
          var expect = 19.8; 
          QuotationCommand command = new QuotationCommand()
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
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              var actual = wholesalerService.GetQuotation(command);
              actual.Should().Be(expect);
          }          
      }

      [Fact]
      public void GetQuotation_Discount20_IsOk()
      {         
          var expect = 44.00;
          QuotationCommand command = new QuotationCommand()
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
          using (var context = new BrasserieContext(ContextOptions))
          {
              var wholesalerService = new WholesalerService(context);

              var actual = wholesalerService.GetQuotation(command);
              actual.Should().Be(expect);
          }
        }

    }
}
