using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Base
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Used as primary key for all entities
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Determine the entity is deleted
        /// </summary>
        public bool Deleted { get; set; } = false;
    }
}
