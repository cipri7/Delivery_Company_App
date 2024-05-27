// using System;
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using MySql.Data.MySqlClient;
// using WebApp.Controllers;
// using Xunit;
//
// namespace WebApp.Tests.Controllers
// {
//     public class SediiControllerTests
//     {
//         [Fact]
//         public void ViewSedii_ReturnsViewWithSedii()
//         {
//             // Arrange
//             var mockReader = new Mock<MySqlDataReader>();
//             mockReader.SetupSequence(r => r.Read())
//                 .Returns(true)
//                 .Returns(false);
//             mockReader.Setup(r => r["oras"]).Returns("CityA");
//             mockReader.Setup(r => r["orar"]).Returns("9");
//
//             var mockCmd = new Mock<MySqlCommand>();
//             mockCmd.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);
//
//             var mockConn = new Mock<MySqlConnection>();
//             mockConn.Setup(m => m.Open());
//             mockConn.Setup(m => m.CreateCommand()).Returns(mockCmd.Object);
//
//             var controller = new SediiController { ConnectionFactory = () => mockConn.Object };
//
//             // Act
//             var result = controller.ViewSedii();
//
//             // Assert
//             var viewResult = Assert.IsType<ViewResult>(result);
//             var sedii = Assert.IsAssignableFrom<List<Tuple<string, string>>>(viewResult.Model);
//             Assert.Single(sedii);
//             Assert.Equal("CityA", sedii[0].Item1);
//             Assert.Equal("9", sedii[0].Item2);
//         }
//     }
// }