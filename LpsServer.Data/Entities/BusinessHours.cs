using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class BusinessHours
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the Day.
        /// </summary>
        public int Day { get; set; }      

        /// <summary>
        /// Gets or sets the OpenTime.
        /// </summary>
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// Gets or sets the CloseTime.
        /// </summary>
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the PauseStart.
        /// </summary>
        public DateTime? PauseStart { get; set; }

        /// <summary>
        /// Gets or sets the PauseEnd.
        /// </summary>
        public DateTime? PauseEnd { get; set; }

        /// <summary>
        /// Gets or sets the CloseTime.
        /// </summary>
        public bool Close { get; set; }
        
        /// <summary>
        ///     Gets or sets the room.
        /// </summary>
        public virtual Room Room { get; set; }
    }
}
