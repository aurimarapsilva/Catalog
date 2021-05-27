using System;

namespace Catalog.Core.Response
{
    /// <summary>
    /// Esta é chamada caso o processo ocorra tudo de acordo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseOk<T> : IResponse
    {
        /// <summary>
        /// Variavel responsavel por montar o objeto ser respondido
        /// </summary>
        private readonly T _responseObj;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseObj"></param>
        public ResponseOk(T responseObj)
        {
            _responseObj = responseObj;
        }

        /// <summary>
        /// Retornara nulo uma vez que não apresenta erro
        /// </summary>
        /// <returns></returns>
        public Error Error()
        {
            return null;
        }

        /// <summary>
        /// Exibe se existe erro na classe
        /// </summary>
        /// <returns>false</returns>
        public bool HasError()
        {
            return false;
        }

        /// <summary>
        /// Exibe o objeto que deve ser respondido
        /// </summary>
        /// <returns></returns>
        public object ResponseObj()
        {
            return _responseObj;
        }
    }
}
