using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class KitchenMenu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KitchenMenu"/> class.
        /// </summary>
        public KitchenMenu()
        {         
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Price
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        ///     Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the Room.
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets the KitchenMenuType.
        /// </summary>
        public virtual KitchenMenuType KitchenMenuType { get; set; }
    }
}
