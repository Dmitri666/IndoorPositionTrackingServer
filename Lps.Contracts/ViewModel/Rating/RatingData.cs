using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Rating
{
    public class RatingData
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the User Name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the Rating
        /// </summary>
        public int State { get; set; }        

        /// <summary>
        /// Gets or sets the Room.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        ///     Gets or sets the Time
        /// </summary>
        public DateTime Time { get; set; }
    }
}
