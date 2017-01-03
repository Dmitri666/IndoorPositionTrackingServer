using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class Rating
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        public Rating()
        {         
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the Rating
        /// </summary>
        public int State { get; set; }

        /// <summary>
        ///     Gets or sets the Time
        /// </summary>
        public DateTime Time { get; set; }
       
        /// <summary>
        /// Gets or sets the Room.
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        public virtual User User { get; set; }
    }
}
