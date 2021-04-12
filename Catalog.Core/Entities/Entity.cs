using System;
using Flunt.Notifications;

namespace Catalog.Core.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
        }
        public string Id { get; private set; }

    }
}