using System.ComponentModel.DataAnnotations;

namespace LuizaLabs.Models
{
    //Entidade da classe produto
    public class Produtos
    {
        #region Construtor

        public Produtos() { }

        #endregion

        #region Propriedades

        /// <summary>
        /// Identificacao do produto
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        [Required(ErrorMessage = "Campo nome do produto é obrigatório"), MaxLength(25, ErrorMessage = "Campo nome do produto não pode ter mais que 25 caracteres")]
        public string Name { get; set; }

        #endregion
    }
}