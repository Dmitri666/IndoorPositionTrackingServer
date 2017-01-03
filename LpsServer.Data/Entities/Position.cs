// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Position.cs" company="">
//   
// </copyright>
// <summary>
//   The position.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.Entities
{
    using System;

    /// <summary>
    /// The position.
    /// </summary>
    public class Position
    {
        // Properties
        // Primary key
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual Device Device { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public double Y { get; set; }

        #endregion

        // Reference
    }
}