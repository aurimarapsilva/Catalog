using System;

namespace Catalog.Core.Response
{
    /// <summary>
    /// Classe responsavel por criar e exibir os erros
    /// </summary>
    public class ResponseError<T> : IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Error _error;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="error"></param>
        public ResponseError(Error error)
        {
            _error = error;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorCode">400</param>
        /// <param name="description">Erro ao consultar a base de dados</param>
        /// <param name="data"></param>
        public ResponseError(string errorCode, string description, object data)
        {
            _error = new Error(code: errorCode,
                            description: description,
                            data: data);
        }

        /// <summary>
        /// Retorna o erro
        /// </summary>
        /// <returns>
        /// {
        ///   Code = 400,
        ///   Description = Erro ao consultar a base de dados,
        ///   Data = null
        /// }
        /// </returns>
        public Error Error()
        {
            return _error;
        }
        
        /// <summary>
        /// Informa o hasError que contem erro
        /// </summary>
        /// <returns>true</returns>
        public bool HasError()
        {
            return true;
        }

        /// <summary>
        /// Retorna uma notificação que não a objeto a ser respondido
        /// </summary>
        /// <returns>Objeto não retornado porquê o Response contém erro.</returns>
        public object ResponseObj()
        {
            return "Objeto não retornado porquê o Response contém erro.";
        }
    }
}
