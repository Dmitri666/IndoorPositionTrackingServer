using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    /// <summary>
    /// Photo
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        ///     Gets or sets the Is Main
        /// </summary>        
        public bool IsMain { get; set; }

        /// <summary>
        ///     Gets or sets the Image
        /// </summary>        
        public string Image { get; set; }

        // Reference
        /// <summary>
        ///     Gets or sets the room.
        /// </summary>
        public virtual Room Room { get; set; }
    }
}
