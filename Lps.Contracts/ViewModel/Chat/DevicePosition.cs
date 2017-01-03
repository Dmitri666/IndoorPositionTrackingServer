// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevicePosition.cs" company="">
//   
// </copyright>
// <summary>
//   The device position.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel.Chat
{
    using System;

    /// <summary>
    /// The device position.
    /// </summary>
    public class DevicePosition
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the device id.
        /// </summary>
        public string DeviceId { get; set; }

        public Guid RoomId { get; set; }

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        public double Y { get; set; }

        #endregion
    }
}