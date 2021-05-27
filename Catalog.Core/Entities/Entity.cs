using System;
using Flunt.Notifications;

namespace Catalog.Core.Entities
{
    /// <summary>
    /// Classe modelo que cuida do id da entidade 
    /// </summary>
    public abstract class Entity : Notifiable, IEntity
    {
        /// <summary>
        /// Id das classe modelos
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int Id { get; private set; }

    }
}