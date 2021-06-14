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
        /// <example>400</example>
        public string Code { get; private set; }

        /// <summary>
        /// Descrição do erro.
        /// </summary>
        /// <example>Erro ao executar a função</example>
        public string Description { get; private set; }

        /// <summary>
        /// Dados o pq aconteceu o erro
        /// </summary>
        /// <example> </example>
        public object Data { get; private set; }
    }
}
