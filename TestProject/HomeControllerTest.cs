using Exercise.Controllers;
using Exercise.Model;
using Exercise.Service;
using Exercise.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    public class HomeControllerTest
    {
        private readonly Mock<IOilService> _oilServiceMock;

        public HomeControllerTest()
        {
            _oilServiceMock = new Mock<IOilService>();
            CommonConfigVariables.ListOilPrices =
            [
                new OilPrice()
                {
                    Date = new DateTime(2020, 02, 03),
                    Price = 54
                },
                new OilPrice()
                {
                    Date = new DateTime(2020, 02, 04),
                    Price = 53.9
                },
                new OilPrice()
                {
                    Date = new DateTime(2020, 02, 05),
                    Price = 55.36
                },
                new OilPrice()
                {
                    Date = new DateTime(2020, 02, 06),
                    Price = 55.18
                },
                new OilPrice()
                {
                    Date = new DateTime(2020, 02, 07),
                    Price = 54.53
                }
            ];
        }

        [Fact]
        public void Get_WithValidInput_WithParams()
        {
            var homeController = new HomeController(_oilServiceMock.Object);

            _oilServiceMock.Setup(x => x.GetByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<OilPrice>());

            var request = new GeneralRequest
            {
                Id = 1,
                JsonRpc = "2.0",
                Method = "GetOilPriceTrend",
                Params = new
                {
                    startDateISO8601 = "2020-02-03",
                    endDateISO8601 = "2020-02-05",
                }
            };

            var result = homeController.Get(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.NotNull(((GetOilPriceTrendResponse)((OkObjectResult)result).Value).Id);
            Assert.NotNull(((GetOilPriceTrendResponse)((OkObjectResult)result).Value).JsonRpc);
            Assert.NotNull(((GetOilPriceTrendResponse)((OkObjectResult)result).Value).Result);
        }

        [Fact]
        public void Get_WithWrongInput_NoParams_Exception()
        {
            var homeController = new HomeController(_oilServiceMock.Object);

            _oilServiceMock.Setup(x => x.GetByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<OilPrice>());

            var request = new GeneralRequest
            {
                Id = 1,
                JsonRpc = "2.0",
                Method = "GetOilPriceTrend",
                Params = new object()
            };

            var result = homeController.Get(request);

            // Assert
            AssertException(result);
        }

        [Theory]
        [MemberData(nameof(WithWrongInputParameters))]
        public void Get_WithWrongInput_Exception(int? id, string jsonRpc, string method)
        {
            var homeController = new HomeController(_oilServiceMock.Object);

            _oilServiceMock.Setup(x => x.GetByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<OilPrice>());

            var request = new GeneralRequest
            {
                Id = id,
                JsonRpc = jsonRpc,
                Method = method,
                Params = new object()
            };
            var result = homeController.Get(request);

            // Assert
            AssertException(result);
        }

        [Theory]
        [InlineData(0, "", "testMethod")]
        public void Get_WithWrongInput_BadRequestException(int? id, string jsonRpc, string method)
        {
            var homeController = new HomeController(_oilServiceMock.Object);

            _oilServiceMock.Setup(x => x.GetByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<OilPrice>());

            var request = new GeneralRequest
            {
                Id = id,
                JsonRpc = jsonRpc,
                Method = method,
                Params = new object()
            };
            var result = homeController.Get(request);

            // Assert
            AssertException(result, true);
        }

        private void AssertException(IActionResult result, bool badRequestObjectResult = false)
        {
            Assert.IsAssignableFrom<ObjectResult>(result);

            if (badRequestObjectResult)
            {
                Assert.Equal(400, ((ObjectResult)result).StatusCode);
                Assert.NotNull(((ExceptionResponse)((BadRequestObjectResult)result).Value).ErrorMessage);
            }
            else
            {
                Assert.Equal(500, ((ObjectResult)result).StatusCode);
                Assert.NotNull(((ExceptionResponse)((ObjectResult)result).Value).ErrorMessage);
            }
        }

        public static IEnumerable<object[]> WithWrongInputParameters =>
            new List<object[]>
            {
                new object[] { null, null, null},
                new object[] { null, "null", null},
                new object[] { null, null, "null"},
                new object[] { 0, null, null},
                new object[] { 0, "null", null},
                new object[] { 0, null, "null"}
            };
    }
}