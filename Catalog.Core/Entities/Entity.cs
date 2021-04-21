using System;
using Flunt.Notifications;

namespace Catalog.Core.Entities
{
    public abstract class Entity : Notifiable, IEntity
    {
        public int Id { get; private set; }

    }
}