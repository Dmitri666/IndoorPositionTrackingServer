// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Actor.cs" company="">
//   
// </copyright>
// <summary>
//   The actor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Chat
{
    using System;

    /// <summary>
    ///     The actor.
    /// </summary>
    public class Actor
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public DevicePosition Position { get; set; }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        public string PhotoPath { get; set; }

        #endregion
    }
}