using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Files
{
    public class PhotoDataResponse
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the Image
        /// </summary>
        public PhotoData Photo { get; set; }
    }
}
