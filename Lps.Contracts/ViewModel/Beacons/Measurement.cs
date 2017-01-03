// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Measurement.cs" company="">
//   
// </copyright>
// <summary>
//   The measurement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Beacons
{
    /// <summary>
    ///     The measurement.
    /// </summary>
    public class Measurement
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        ///     Gets or sets the distance.
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        ///     Gets or sets the running average rssi.
        /// </summary>
        public double RunningAverageRssi { get; set; }

        /// <summary>
        ///     Gets or sets the tx power.
        /// </summary>
        public int TxPower { get; set; }

        #endregion
    }
}