using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace LuizaLabs.Controllers
{
    public class WishesController : ApiController
    {
        #region Verbos

        /// <summary>
        /// Obter os desejos com paginacao
        /// </summary>
        /// <param name="page_size">Itens por paginacao</param>
        /// <param name="page">Numero de pagina</param>
        /// <returns>Desejos encontrados no range de paginacao</returns>
        [HttpGet]
        [Route("api/Wishes/{userID}")]
        public IHttpActionResult Get(int userID, [FromUri] int page_size, [FromUri] int page)
        {
            List<Models.Produtos> desejos = ObterDesejos(userID);

            //Informar nao encontrado caso a lista esteja zerada
            if (desejos.Count == 0)
                return NotFound();

            //Recuperar desejos de acordo com a paginacao
            var desejosPaginacao = desejos.OrderBy(o => o.ID).Skip(page_size * (page - 1)).Take(page_size);

            //Adicionar no header a pagina
            System.Web.HttpContext.Current.Response.AddHeader("X-Pages-TotalPages", page_size.ToString());

            return Ok(desejosPaginacao);
        }

        /// <summary>
        /// Criar desejo do usuario
        /// </summary>
        /// <param name="userID">ID do usuario</param>
        /// <param name="produtos">Lista de produtos</param>
        /// <returns>ID dos produtos inseridos nos desejos</returns>
        [HttpPost]
        [Route("api/Wishes/{userID}")]
        [ResponseType(typeof(Models.Desejos))]
        public IHttpActionResult Post(int userID, [FromBody] List<Models.Produtos> produtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Apenas uma simulacao de inclusao na base via EF ou qualquer outra forma e considerando que o ID seja incremental
                List<Models.Desejos> desejos = new List<Models.Desejos>();

                //Simulando ID gerado no insert
                foreach (var prod in produtos)
                    desejos.Add(new Models.Desejos() { UserID = userID, ProductID = prod.ID });

                return CreatedAtRoute("DefaultApi", new
                {
                    controller = "Wishes",
                    id = userID
                }, desejos);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Erro ao cadastrar os desejos na base de dados: {ex.InnerException}"));
            }
        }

        /// <summary>
        /// Deletar desejo do usuario
        /// </summary>
        /// <param name="userID">Id do usuario</param>
        /// <param name="productID">Id do produto</param>
        /// <returns>Entidade deletada</returns>
        [HttpDelete]
        [Route("api/Wishes/{userID}/{productID}")]
        [ResponseType(typeof(Models.Desejos))]
        public IHttpActionResult Delete(int userID, int productID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Apenas uma simulacao de que a lista fosse uma representacao da tabela com os valores
                List<Models.Desejos> desejos = new List<Models.Desejos>();
                desejos.Add(new Models.Desejos() { ProductID = 1, UserID = 1 });
                desejos.Add(new Models.Desejos() { ProductID = 3, UserID = 1 });

                var desejo = desejos.Where(p => p.UserID == userID & p.ProductID == productID).FirstOrDefault();
                desejos.Remove(desejo);

                return CreatedAtRoute("DefaultApi", new
                {
                    controller = "Wishes",
                    id = userID
                }, desejo);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Erro ao cadastrar os desejos na base de dados: {ex.InnerException}"));
            }
        }

        #endregion

        #region Metodos privados

        /// <summary>
        /// Obter lista de desejos
        /// </summary>
        /// <returns>Lista de Produtos/desejos do usuario</returns>
        List<Models.Produtos> ObterDesejos(int UserID)
        {
            List<Models.Desejos> desejos = new List<Models.Desejos>();

            //Adicionando produto Batedeira aos desejos do usuario 1
            desejos.Add(new Models.Desejos() { UserID = 1, ProductID = 1 });

            //Adicionando produto Video Cassete aos desejos do usuario 1
            desejos.Add(new Models.Desejos() { UserID = 1, ProductID = 3 });

            //Join das entidades para recuperar os produtos desejados
            var retorno = (from d in desejos
                           join p in ObterProdutos() on d.ProductID equals p.ID
                           where d.UserID == UserID
                           select p).OrderBy(p => p.ID);

            return retorno.ToList();
        }

        /// <summary>
        /// Obter lista de produtos
        /// </summary>
        /// <returns>Lista de produtos</returns>
        List<Models.Produtos> ObterProdutos()
        {
            List<Models.Produtos> retorno = new List<Models.Produtos>();

            //Adicionando produto Batedeira
            retorno.Add(new Models.Produtos() { ID = 1, Name = "Batedeira" });

            //Adicionando produto Video Casset
            retorno.Add(new Models.Produtos() { ID = 2, Name = "Video Cassete" });

            //Adicionando produto Toca Fitas
            retorno.Add(new Models.Produtos() { ID = 3, Name = "Toca Fitas" });

            return retorno;
        }

        #endregion
    }
}
