using Brasserie.Core.Domains;
using Brasserie.Data.Repositories.Interfaces;
using Brasserie.Service.Beers.Services;
using Brasserie.Service.Brewers.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace UnitTesting
{
    
    public class BeerUnitTests
    {
               
        [Fact]
        public void Test1()
        {
            Beer expect = new Beer() { Id = 1, Name = "Jawad", AlcoholPercentage = 16, Price = 10 };         
            externalServiceClientMock.Setup(x => x.FindById(1)).Returns(expect);
            Beer actual = myService.FindById(1);                          
            externalServiceClientMock.Verify(x => x.FindById(1));
            Assert.AreEqual(expect, actual);
        }

        [SetUp]
        [Fact]
        public void MyServiceSetup()
        {
            externalServiceClientMock = new Mock<IBeerRepository>();
            myService = new BeerService(externalServiceClientMock.Object);
                        
        }

        private Mock<IBeerRepository> externalServiceClientMock;
        private BeerService myService;
    }
}
