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
        private readonly int _responseSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseObj"></param>
        /// <param name="responseSize"></param>
        public ResponseOk(T responseObj, int responseSize = 0)
        {
            _responseObj = responseObj;
            _responseSize = responseSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _responseSize;
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
