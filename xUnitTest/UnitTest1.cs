using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using web.Controllers;
using web.Db;
using web.Models;

namespace xUnitTest
{
    // https://learn.microsoft.com/ru-ru/dotnet/core/testing/unit-testing-with-dotnet-test

    public class UnitTest1
    {
        [Fact]
        public async Task Index_ReturnsAViewResult()
        {

            // Arrange
            var serviceProvider = new ServiceCollection()
               .AddLogging()
               .BuildServiceProvider();

            var factoryContext = serviceProvider.GetService <MyContext>();
            
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HomeController>();

            var option = Options.Create(new QRPulseConfig());

            var controller = new HomeController(logger, option, factoryContext);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            
        }

        [Fact]
        public async Task Login_ReturnsAViewResult()
        {

            // Arrange
            var serviceProvider = new ServiceCollection()
               .AddLogging()
               .BuildServiceProvider();

            var factoryContext = serviceProvider.GetService<MyContext>();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<HomeController>();

            var option = Options.Create(new QRPulseConfig());

            var controller = new HomeController(logger, option, factoryContext);

            // Act
            var result = controller.Login("");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            Assert.False((bool?) viewResult.ViewData["Loggedin"]);
        }

        [Fact]
        public async Task AdminReview_Index_ReturnsAViewResult()
        {

            // Arrange
            var serviceProvider = new ServiceCollection()
               .AddLogging()
               .BuildServiceProvider();

            var factoryContext = serviceProvider.GetService<MyContext>();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<AdminReviewPageController>();

            var option = Options.Create(new QRPulseConfig());


            var controller = new AdminReviewPageController(logger, option, factoryContext);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);

        }


        [Fact]
        public async Task ReviewAsk_ThankYou_ReturnsAViewResult()
        {

            // Arrange
            var serviceProvider = new ServiceCollection()
               .AddLogging()
               .BuildServiceProvider();

            var factoryContext = serviceProvider.GetService<MyContext>();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<ReviewAskController>();

            var option = Options.Create(new QRPulseConfig());


            var controller = new ReviewAskController(logger, option, factoryContext);

            // Act
            var result = controller.ThankYou();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }


    }
}