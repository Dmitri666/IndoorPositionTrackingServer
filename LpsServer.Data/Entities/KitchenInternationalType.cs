using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{    
    public class KitchenInternationalType
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KitchenInternationalType"/> class.
        /// </summary>
        public KitchenInternationalType()
        {
            this.RoomKitchenInternationalList = new HashSet<RoomKitchenInternational>();            
        }

        #endregion

        // Properties
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the ParentId.
        /// </summary>
        public Guid ParentId { get; set; }

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
        public virtual ICollection<RoomKitchenInternational> RoomKitchenInternationalList { get; set; }     

        #endregion
    }
}
