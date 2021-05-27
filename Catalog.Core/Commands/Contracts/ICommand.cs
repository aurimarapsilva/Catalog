namespace Catalog.Core.Commands.Contracts
{
    /// <summary>
    /// Interface responsavel por todos os comandos de entrada da API
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Valida se o comando atendeu todos os requesitos
        /// </summary>
        void Validate();
    }
}