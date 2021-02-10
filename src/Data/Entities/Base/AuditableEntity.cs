using System;

namespace Data.Entities.Base
{
    public abstract class AuditableEntity : BaseEntity
    {
        /// <summary>
        /// Created date of an entity
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Created customer of an entity
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Last modified date of an entity
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Last modified customer of an entity
        /// </summary>
        public int LastModifiedBy { get; set; }
    }
}
