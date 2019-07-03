using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace LuizaLabs.Controllers
{
    public class UsersController : ApiController
    {
        #region Verbos

        /// <summary>
        /// Obter os usuarios com paginacao
        /// </summary>
        /// <param name="page_size">Itens por paginacao</param>
        /// <param name="page">Numero de pagina</param>
        /// <returns>Usuarios encontrados no range de paginacao</returns>
        public IHttpActionResult Get([FromUri] int page_size, [FromUri] int page)
        {
            List<Models.Usuarios> usuarios = ObterUsuarios();

            //Informar nao encontrado caso a lista esteja zerada
            if (usuarios.Count == 0)
                return NotFound();

            //Recuperar usuarios de acordo com a paginacao
            var usuariosPaginacao = usuarios.OrderBy(o => o.ID).Skip(page_size * (page - 1)).Take(page_size);

            //Adicionar no header a pagina
            System.Web.HttpContext.Current.Response.AddHeader("X-Pages-TotalPages", page_size.ToString());

            return Ok(usuariosPaginacao);
        }

        /// <summary>
        /// Criar usuario
        /// </summary>
        /// <param name="usuario">Entidade usuario</param>
        /// <returns>Usuario criado</returns>
        [ResponseType(typeof(Models.Usuarios))]
        public IHttpActionResult Post([FromBody] Models.Usuarios usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //Apenas uma simulacao de inclusao na base via EF ou qualquer outra forma e considerando que o ID seja incremental
                List<Models.Usuarios> usuarios = ObterUsuarios();
                //Validar se o produto ja existe
                if (usuarios.Any(p => p.Name.Equals(usuario.Name, StringComparison.OrdinalIgnoreCase)))
                    return Content(HttpStatusCode.Conflict, "Usuário já cadastrado na base de dados");

                usuarios.Add(usuario);

                //Simulando ID gerado no insert
                usuario.ID = 4;

                return CreatedAtRoute("DefaultApi", new
                {
                    id = usuario.ID
                }, usuario);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Erro ao cadastrar o usuario na base de dados: {ex.InnerException}"));
            }
        }

        #endregion

        #region Metodos privados

        /// <summary>
        /// Obter lista de usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        List<Models.Usuarios> ObterUsuarios()
        {
            List<Models.Usuarios> retorno = new List<Models.Usuarios>();

            //Adicionando usuario Rodrigo
            retorno.Add(new Models.Usuarios() { ID = 1, Name = "Rodrigo Carvalho", Email = "rodrigo@luizalabs.com" });

            //Adicionando usuario Marcel
            retorno.Add(new Models.Usuarios() { ID = 2, Name = "Marcel Grilo", Email = "marcel@luizalabs.com" });

            //Adicionando usuario Alexandre
            retorno.Add(new Models.Usuarios() { ID = 3, Name = "Alexandre Faria", Email = "alexandre@luizalabs.com" });

            return retorno;
        }

        #endregion
    }
}
