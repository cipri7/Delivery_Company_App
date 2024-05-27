using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Models;
using Xunit;

namespace WebApp.Tests.Controllers
{
    public class RuteControllerTests
    {
        [Fact]
        public async Task Register_ValidModel_RedirectsToSuccess()
        {
            // Arrange
            var controller = new RuteController();
            var newSediu = new AdaugaRutaViewModel
            {
                Oras = "CityA",
                Orar = "9"
            };

            // Act
            var result = await controller.Register(newSediu);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Success", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Register_InvalidModel_ReturnsViewWithError()
        {
            // Arrange
            var controller = new RuteController();
            controller.ModelState.AddModelError("Oras", "Oras is required");
            var invalidSediu = new AdaugaRutaViewModel();

            // Act
            var result = await controller.Register(invalidSediu);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        
    }
}
