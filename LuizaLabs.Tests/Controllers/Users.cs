using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuizaLabs.Controllers;
using System.Net;

namespace LuizaLabs.Tests.Controllers
{
    [TestClass]
    public class Users
    {
        /// <summary>
        /// Deveria retornar duplicidade
        /// </summary>
        [TestMethod]
        public void DeveriaRetornarConflito()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            var result = controller.Post(new Models.Usuarios() { Name = "Rodrigo Carvalho" });

            // Assert
            Assert.AreEqual(((System.Web.Http.Results.NegotiatedContentResult<string>)result).StatusCode, HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Deveria retornar entidade criada
        /// </summary>
        [TestMethod]
        public void DeveriaRetornarEntidadeCriada()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            var result = controller.Post(new Models.Usuarios() { Name = "Usuario novo" });

            // Assert
            Assert.IsTrue(((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<LuizaLabs.Models.Usuarios>)result).Content != null);
        }
    }
}
