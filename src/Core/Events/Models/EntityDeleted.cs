using Data.Entities.Base;

namespace Core.Events.Models
{
    /// <summary>
    /// A container for entities that have been deleted.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDeleted<T> where T : BaseEntity
    {
        public EntityDeleted(T entity)
        {
            this.Entity = entity;
        }

        public T Entity { get; private set; }
    }
}
