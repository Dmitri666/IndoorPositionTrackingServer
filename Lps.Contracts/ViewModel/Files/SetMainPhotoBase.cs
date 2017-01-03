namespace Lps.Contracts.ViewModel.Files
{
    using System;

    public class SetMainPhotoBase
    {      
        /// <summary>
        /// Gets or sets the PhotoId
        /// </summary>
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the isMain
        /// </summary>
        public bool IsMain { get; set; }
    }
}
