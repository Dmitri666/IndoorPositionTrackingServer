namespace LpsServer.Data.Entities
{
    using System;

    /// <summary>
    /// Photo
    /// </summary>
    public class UserPhoto
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
        ///     Gets or sets the User.
        /// </summary>
        public virtual User User { get; set; }
    }
}
