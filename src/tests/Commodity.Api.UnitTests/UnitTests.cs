using Commodity.Api.Controllers;
using Commodity.Api.Models;
using Commodity.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Commodity.Api.UnitTests
{
    public class Tests
    {
        private Mock<ICommodityService> _commodityServiceMock;

        [SetUp]
        public void Setup()
        {
            _commodityServiceMock = new Mock<ICommodityService>();
        }

         [Test]
        public async Task GetCommodityById_ShouldReturn200_AndExpectedCommodity()
        {
            // Arrange

            var expectedCommodity = new CommodityModel { Id = 1, Name = "Test Article 1", Price = 10.0m, Category = "Test Category", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            _commodityServiceMock.Setup(x => x.GetCommodityByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedCommodity);

            var sut = new CommodityController(_commodityServiceMock.Object);

            //Act

            var response = await sut.GetCommodity(1);

            var actionResult = response.Result as OkObjectResult;

            //Assert

            Assert.That(actionResult, Is.Not.Null);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actionResult.StatusCode, Is.EqualTo(200));
                Assert.That(actionResult.Value, Is.InstanceOf<CommodityModel>());
                //Assert.That(actionResult.Value as CommodityModel, Is.EquivalentTo(expectedCommodity));
            }
        }

        [Test]
        public async Task GetAllCommodities_ShouldReturn200_AndContainAllCommodities()
        {
            // Arrange

            var expectedCommodities = new[] {
                new CommodityModel { Id = 1, Name = "Test Article 1", Price = 10.0m, Category = "Test Category", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new CommodityModel { Id = 2, Name = "Test Article 2", Price = 20.0m, Category = "Test Category", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };

            _commodityServiceMock.Setup(x => x.GetAllCommoditiesAsync()).ReturnsAsync(expectedCommodities);

            var sut = new CommodityController(_commodityServiceMock.Object);

            //Act

            var response = await sut.GetAllCommodities();

            var actionResult = response.Result as OkObjectResult;

            //Assert

            Assert.That(actionResult, Is.Not.Null);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actionResult.StatusCode, Is.EqualTo(200));
                Assert.That(actionResult.Value, Is.InstanceOf<IEnumerable<CommodityModel>>());
                Assert.That(actionResult.Value as IEnumerable<CommodityModel>, Is.EquivalentTo(expectedCommodities));
            }
        }
    }
}