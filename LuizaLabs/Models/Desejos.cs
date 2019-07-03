using System.Runtime.Serialization;

namespace LuizaLabs.Models
{
    //Entidade da classe produto
    public class Desejos
    {
        #region Construtor

        public Desejos() { }

        #endregion

        #region Propriedades

        /// <summary>
        /// Id do usuario
        /// </summary>
        [IgnoreDataMember]
        public int UserID { get; set; }

        /// <summary>
        /// ID do produto
        /// </summary>
        public int ProductID { get; set; }

        #endregion
    }
}