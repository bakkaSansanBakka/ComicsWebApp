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
            Assert.IsAssignableFrom<ListOfComicsViewModel>(viewResult.Model);
        }

        [Fact]
        public void AddComicsReturnsViewResultWithAddEditModel()
        {
            // Arrange

            // Act
            var result = controller.AddComics();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ComicsAddEditModel>(viewResult.Model);
        }

        [Fact]
        public void ComicsInfoReturnsViewResultWithComicsViewModel()
        {
            // Arrange
            int comicsId = 4;

            // Act
            var result = controller.ComicsInfo(comicsId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ComicsViewModel>(viewResult.Model);
        }

        [Fact]
        public void RenderPhotoReturnsFileWhenComicsFound()
        {
            // Arrange
            int comicsId = 5;

            // Act
            var result = controller.RenderPhoto(comicsId);

            // Assert
            Assert.IsType<FileContentResult>(result);
        }

        [Fact]
        public void RenderPhotoReturnsIndexViewResultWhenComicsNotFound()
        {
            // Arrange
            int comicsId = -1;

            // Act
            var result = controller.RenderPhoto(comicsId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", viewResult.ViewName);
        }
    }
}