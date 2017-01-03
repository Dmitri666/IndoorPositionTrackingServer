// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoomInfo.cs" company="">
//   
// </copyright>
// <summary>
//   The map info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Rooms
{
    using System;

    /// <summary>
    ///     The map info.
    /// </summary>
    public class RoomInfo
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomInfo" /> class.
        /// </summary>
        public RoomInfo()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the image file name.
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is chat exist.
        /// </summary>
        public bool IsChatExist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is favorite.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        ///     Gets or sets the lat.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        ///     Gets or sets the lng.
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        public double Rating { get; set; }

        #endregion
    }
}