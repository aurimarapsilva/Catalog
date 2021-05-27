namespace Catalog.Core.Response
{
    /// <summary>
    /// Classe responsavel pelo os erros
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="data"></param>
        public Error(string code, string description, object data)
        {
            Code = code;
            Description = description;
            Data = data;
        }

        /// <summary>
        /// Código do erro.
        /// </summary>
        public string Code { get; private set; }
        
        /// <summary>
        /// Descrição do erro.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Dados o pq aconteceu o erro
        /// </summary>
        public object Data { get; set; }

    }
}
