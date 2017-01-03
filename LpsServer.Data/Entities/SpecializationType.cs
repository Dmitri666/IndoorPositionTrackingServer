namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class SpecializationType
    {        
        public SpecializationType()
        {
            this.SpecializationList = new HashSet<Specialization>();            
        }       

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        ///     Gets or sets the Order.
        /// </summary>
        public int Hierarchie { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description 2 d.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     Gets or sets the room item list.
        /// </summary>
        public virtual ICollection<Specialization> SpecializationList { get; set; }            
    }
}
