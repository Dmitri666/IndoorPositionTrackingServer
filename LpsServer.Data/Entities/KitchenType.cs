using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{    
    public class KitchenType
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemType"/> class.
        /// </summary>
        public KitchenType()
        {
            this.KitchenTypeList = new HashSet<RoomKitchen>();            
        }

        #endregion

        // Properties
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

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
        public virtual ICollection<RoomKitchen> KitchenTypeList { get; set; }     

        #endregion
    }
}
