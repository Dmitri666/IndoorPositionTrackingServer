// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangingData.cs" company="">
//   
// </copyright>
// <summary>
//   The ranging data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel.Beacons
{
    using System.Collections.Generic;

    /// <summary>
    /// The ranging data.
    /// </summary>
    public class RangingData
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RangingData"/> class.
        /// </summary>
        public RangingData()
        {
            this.BeaconDataList = new List<BeaconData>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the beacon data list.
        /// </summary>
        public List<BeaconData> BeaconDataList { get; set; }

        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        public string DeviceId { get; set; }

        #endregion
    }
}