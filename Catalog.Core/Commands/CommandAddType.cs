using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandAddType : Notifiable, ICommand
    {
        public string Type { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Type, "CatalogType.Type", "Deve-se informar o tipo para realizar o cadastro")
            .HasMaxLengthIfNotNullOrEmpty(Type, 100, "CatalogType.Type", "Tamanho 100 caracteres"));
        }
    }
}