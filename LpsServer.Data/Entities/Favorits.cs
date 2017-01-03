// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Favorits.cs" company="">
//   
// </copyright>
// <summary>
//   The favorits.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The favorits.
    /// </summary>
    public class Favorits
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Bookings
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        ///     Bookings
        /// </summary>
        public virtual User User { get; set; }

        #endregion
    }
}