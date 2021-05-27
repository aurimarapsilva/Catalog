namespace Catalog.Core.Response
{
    /// <summary>
    /// Interface para quaisquer tipo de resposta
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponse
    {
        /// <summary>
        /// Objeto a ser respondido
        /// </summary>
        /// <returns></returns>
        object ResponseObj();

        /// <summary>
        /// Se possui erro na classe
        /// </summary>
        /// <returns></returns>
        bool HasError();

        /// <summary>
        /// Erro a ser respondido
        /// </summary>
        /// <returns></returns>
        Error Error();
    }
}
