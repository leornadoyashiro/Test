using System.ComponentModel.DataAnnotations;

namespace LuizaLabs.Models
{
    //Entidade da classe usuario
    public class Usuarios
    {
        #region Construtor

        public Usuarios() { }

        #endregion

        #region Propriedades

        /// <summary>
        /// Identificacao do usuario
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome do usuario
        /// </summary>
        [Required(ErrorMessage = "Campo nome do usuário é obrigatório"), MaxLength(50, ErrorMessage = "Campo nome do usuário não pode ter mais que 50 caracteres")]
        public string Name { get; set; }

        /// <summary>
        /// Email do usuario
        /// </summary>
        [Required(ErrorMessage = "Campo e-mail é obrigatório"), MaxLength(30, ErrorMessage = "Campo e-mail não pode ter mais que 30 caracteres")]
        public string Email { get; set; }

        #endregion
    }
}