using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Files
{
    public class PhotoData
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
    }
}
