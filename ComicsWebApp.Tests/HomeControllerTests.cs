using AutoMapper;
using ComicsWebApp.Controllers;
using ComicsWebApp.Data;
using ComicsWebApp.Data.Models;
using ComicsWebApp.Models;
using ComicsWebApp.Utilities;
using EntityFrameworkCore.Testing.Moq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ComicsWebApp.Tests
{
    public class HomeControllerTests
    {
        private HomeController controller;

        public HomeControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            var mockLogger = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mockLogger.Object;
            var mockDbContext = Create.MockedDbContextFor<ComicsDbContext>();
            var mockValidator = Mock.Of<IValidator<ComicsAddEditModel>>();

            controller = new HomeController(logger, mockDbContext, mapper, mockValidator);
        }

        [Fact]
        public void IndexReturnsViewResultWithListOfComicsViewModel()
        {
            // Arrange

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ListOfComicsViewModel>(viewResult.Model);
        }
    }
}