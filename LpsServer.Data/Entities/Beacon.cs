// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Beacon.cs" company="">
//   
// </copyright>
// <summary>
//   The beacon.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     The beacon.
    /// </summary>
    [Table("Beacon")]
    public class Beacon
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 1.
        /// </summary>
        public Guid Identifier1 { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 2.
        /// </summary>
        public int Identifier2 { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 3.
        /// </summary>
        [Required]
        [Index("IX_Identifier3Room", IsClustered = false, Order = 1)]
        public int Identifier3 { get; set; }

        /// <summary>
        ///     Gets or sets the room.
        /// </summary>
        [Required]
        [Index("IX_Identifier3Room", IsClustered = false, Order = 2)]
        public virtual Room Room { get; set; }

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        [Required]
        public double X { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        [Required]
        public double Y { get; set; }
        
        #endregion
    }
}