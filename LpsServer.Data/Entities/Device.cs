// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Device.cs" company="">
//   
// </copyright>
// <summary>
//   The messungen.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     The messungen.
    /// </summary>
    [Table("Device")]
    public class Device
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        public Device()
        {
            this.PositiontList = new HashSet<Position>();
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        ///     Gets or sets the build number.
        /// </summary>
        [Key]
        [MaxLength(30)]
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        // Reference
        /// <summary>
        ///     Gets or sets the positiont list.
        /// </summary>
        public virtual ICollection<Position> PositiontList { get; set; }

        #endregion
    }
}