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
    [Table("BeaconInRange")]
    public class BeaconInRange
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 1.
        /// </summary>
        [Required]
        public Guid Identifier1 { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 2.
        /// </summary>
        [Required]
        public int Identifier2 { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 3.
        /// </summary>
        [Required]
        public int Identifier3 { get; set; }

        #endregion
    }
}