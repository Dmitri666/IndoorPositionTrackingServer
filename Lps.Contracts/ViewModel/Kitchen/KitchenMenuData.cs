using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Kitchen
{
    public class KitchenMenuData
    {        
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
        ///     Gets or sets the KitchenMenuTypeId.
        /// </summary>
        public Guid KitchenMenuTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the KitchenMenuTypeId.
        /// </summary>
        public string KitchenMenuTypeName { get; set; }
    }
}
