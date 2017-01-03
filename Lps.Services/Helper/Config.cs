// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="">
//   
// </copyright>
// <summary>
//   The config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Services.Helper
{
    using System.Configuration;

    /// <summary>
    ///     The config.
    /// </summary>
    public static class Config
    {
        #region Static Fields

        /// <summary>
        /// The is last or avarage distance.
        /// </summary>
        private static bool? isLastOrAvarageDistance;

        /// <summary>
        /// The position tracker buffer size.
        /// </summary>
        private static int? positionTrackerBufferSize;

        /// <summary>
        ///     The position tracker timer periode.
        /// </summary>
        private static int? positionTrackerTimerPeriode;

        private static int? measuredBeaconCount;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether is last or avarage distance.
        /// </summary>
        public static bool IsLastOrAvarageDistance
        {
            get
            {
                if (!isLastOrAvarageDistance.HasValue)
                {
                    var boolValue = true;
                    var stringValue = ConfigurationManager.AppSettings.Get("IsLastOrAvarageDistance");
                    if (!string.IsNullOrEmpty(stringValue))
                    {
                        bool.TryParse(stringValue, out boolValue);
                    }

                    isLastOrAvarageDistance = boolValue;
                }

                return isLastOrAvarageDistance.Value;
            }

            set
            {
                isLastOrAvarageDistance = value;
            }
        }

        public static int MeasuredBeaconCount
        {
            get
            {
                if (!measuredBeaconCount.HasValue)
                {
                    var timerPeriode = 0;
                    var periode = ConfigurationManager.AppSettings.Get("MeasuredBeaconCount");
                    if (!string.IsNullOrEmpty(periode))
                    {
                        int.TryParse(periode, out timerPeriode);
                    }

                    measuredBeaconCount = timerPeriode;
                }

                return measuredBeaconCount.Value;
            }

            set
            {
                measuredBeaconCount = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the position tracker buffer size.
        /// </summary>
        public static int PositionTrackerBufferSize
        {
            get
            {
                if (!positionTrackerBufferSize.HasValue)
                {
                    var timerPeriode = 5;
                    var periode = ConfigurationManager.AppSettings.Get("PositionTrackerBufferSize");
                    if (!string.IsNullOrEmpty(periode))
                    {
                        int.TryParse(periode, out timerPeriode);
                    }

                    positionTrackerBufferSize = timerPeriode;
                }

                return positionTrackerBufferSize.Value;
            }

            set
            {
                positionTrackerBufferSize = value;
            }
        }

        /// <summary>
        ///     Gets or sets the position tracker timer periode.
        /// </summary>
        public static int PositionTrackerTimerPeriode
        {
            get
            {
                if (!positionTrackerTimerPeriode.HasValue)
                {
                    var timerPeriode = 5;
                    var periode = ConfigurationManager.AppSettings.Get("PositionTrackerTimerPeriode");
                    if (!string.IsNullOrEmpty(periode))
                    {
                        int.TryParse(periode, out timerPeriode);
                    }

                    positionTrackerTimerPeriode = timerPeriode;
                }

                return positionTrackerTimerPeriode.Value;
            }

            set
            {
                positionTrackerTimerPeriode = value;
            }
        }

        #endregion
    }
}