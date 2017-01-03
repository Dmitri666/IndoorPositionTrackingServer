using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class KitchenMenuType
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KitchenMenuType"/> class.
        /// </summary>
        public KitchenMenuType()
        {
            this.KitchenMenuList = new HashSet<KitchenMenu>();            
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
        ///     Gets or sets the Description
        /// </summary>
        public string Description { get; set; }         

        /// <summary>
        ///     Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     Gets or sets the Kitchen Menu List
        /// </summary>
        public virtual ICollection<KitchenMenu> KitchenMenuList { get; set; }     

        #endregion
    }
}
