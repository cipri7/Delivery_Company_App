using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Models;
using Xunit;

namespace WebApp.Tests.Controllers
{
    public class PachetControllerTests
    {
        [Fact]
        public async Task Create_ValidModel_RedirectsToSuccess()
        {
            // Arrange
            var controller = new PachetController();
            var pachet = new Pachet
            {
                NumeExpeditor = "John Doe",
                TelefonExpeditor = "1234567890",
                OrasPlecare = "CityA",
                OrasDestinatie = "CityB",
                NumeDestinatar = "Jane Doe",
                TelefonDestinatar = "0987654321",
                Greutate = 5.0,
                CategorieSpeciala = "Fragil"
            };

            // Act
            var result = await controller.Create(pachet);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Success", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Success_ReturnsViewWithCost()
        {
            // Arrange
            var controller = new PachetController();
            double cost = 100.0;

            // Act
            var result = controller.Success(cost);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(cost, viewResult.ViewData["CostLivrare"]);
        }

        [Fact]
        public void Accept_ReturnsAcceptedView()
        {
            // Arrange
            var controller = new PachetController();
            double cost = 100.0;

            // Act
            var result = controller.Accept(cost);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Accepted", viewResult.ViewName);
            Assert.Equal(cost, viewResult.ViewData["AcceptedCost"]);
        }
        
        
        //new1
        [Fact]
        public async Task Create_InvalidModel_ReturnsViewWithError()
        {
            // Arrange
            var controller = new PachetController();
            controller.ModelState.AddModelError("NumeExpeditor", "NumeExpeditor is required");
            var invalidPachet = new Pachet();

            // Act
            var result = await controller.Create(invalidPachet);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        //new2
        [Fact]
        public void Success_CostIsNegative_ReturnsBadRequest()
        {
            // Arrange
            var controller = new PachetController();
            double negativeCost = -50.0;

            // Act
            var result = controller.Success(negativeCost);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        //new3
        [Fact]
        public void Accept_CostIsZero_ReturnsAcceptedView()
        {
            // Arrange
            var controller = new PachetController();
            double zeroCost = 0.0;

            // Act
            var result = controller.Accept(zeroCost);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Accepted", viewResult.ViewName);
            Assert.Equal(zeroCost, viewResult.ViewData["AcceptedCost"]);
        }
        
        //new4
        
    }
}