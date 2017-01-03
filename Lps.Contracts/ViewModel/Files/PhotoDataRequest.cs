using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Files
{
    public class PhotoDataRequest
    {
        /// <summary>
        /// Gets or sets the RoomId
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Gets or sets the isMail
        /// </summary>
        public bool isMain { get; set; }

        /// <summary>
        /// Gets or sets the Image
        /// </summary>
        public string Image { get; set; }
    }
}
