// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BeaconData.cs" company="">
//   
// </copyright>
// <summary>
//   The beacon data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel.Beacons
{
    using System;

    /// <summary>
    /// The beacon data.
    /// </summary>
    public class BeaconData
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BeaconData"/> class.
        /// </summary>
        public BeaconData()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the bluetoth address.
        /// </summary>
        public string BluetothAddress { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public double AverageDistance { get; set; }

        /// <summary>
        /// Gets or sets the id 1.
        /// </summary>
        public Guid Id1 { get; set; }

        /// <summary>
        /// Gets or sets the id 2.
        /// </summary>
        public int Id2 { get; set; }

        /// <summary>
        /// Gets or sets the id 3.
        /// </summary>
        public int Id3 { get; set; }

        /// <summary>
        /// Gets or sets the rssi level.
        /// </summary>
        public double AverageRssiLevel { get; set; }

        /// <summary>
        /// Gets or sets the tx power.
        /// </summary>
        public int TxPower { get; set; }

        #endregion
    }
}