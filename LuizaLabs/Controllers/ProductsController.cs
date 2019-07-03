using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace LuizaLabs.Controllers
{
    public class ProductsController : ApiController
    {
        #region Verbos

        /// <summary>
        /// Obter os produtos com paginacao
        /// </summary>
        /// <param name="page_size">Itens por paginacao</param>
        /// <param name="page">Numero de pagina</param>
        /// <returns>Produtos encontrados no range de paginacao</returns>
        public IHttpActionResult Get([FromUri] int page_size, [FromUri] int page)
        {
            List<Models.Produtos> produtos = ObterProdutos();

            //Informar nao encontrado caso a lista esteja zerada
            if (produtos.Count == 0)
                return NotFound();

            //Recuperar produtos de acordo com a paginacao
            var produtosPaginacao = produtos.OrderBy(o => o.ID).Skip(page_size * (page - 1)).Take(page_size);

            //Adicionar no header a pagina
            System.Web.HttpContext.Current.Response.AddHeader("X-Pages-TotalPages", page_size.ToString());

            return Ok(produtosPaginacao);
        }

        /// <summary>
        /// Criar produto
        /// </summary>
        /// <param name="produto">Entidade produto</param>
        /// <returns>Produto criado</returns>
        [ResponseType(typeof(Models.Produtos))]
        public IHttpActionResult Post([FromBody] Models.Produtos produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Apenas uma simulacao de inclusao na base via EF ou qualquer outra forma e considerando que o ID seja incremental
                List<Models.Produtos> produtos = ObterProdutos();

                //Validar se o produto ja existe
                if (produtos.Any(p => p.Name.Equals(produto.Name, StringComparison.OrdinalIgnoreCase)))
                    return Content(HttpStatusCode.Conflict, "Produto já cadastrado na base de dados");

                produtos.Add(produto);

                //Simulando ID gerado no insert
                produto.ID = 4;

                return CreatedAtRoute("DefaultApi", new
                {
                    id = produto.ID
                }, produto);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Erro ao cadastrar o produto na base de dados: {ex.InnerException}"));
            }
        }

        #endregion

        #region Metodos privados

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
