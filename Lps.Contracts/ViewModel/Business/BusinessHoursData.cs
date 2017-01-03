using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Business
{
    public class BusinessHoursData
    {
        /// <summary>
        ///     Gets or sets the Day.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the OpenTime.
        /// </summary>
        public string OpenTime { get; set; }

        /// <summary>
        /// Gets or sets the CloseTime.
        /// </summary>
        public string CloseTime { get; set; }

        /// <summary>
        /// Gets or sets the PauseStart.
        /// </summary>
        public string PauseStart { get; set; }

        /// <summary>
        /// Gets or sets the PauseEnd.
        /// </summary>
        public string PauseEnd { get; set; }

        ///// <summary>
        ///// Gets or sets the PauseStart.
        ///// </summary>
        //public string OpenTimeFormatted { get; set; }

        ///// <summary>
        ///// Gets or sets the PauseEnd.
        ///// </summary>
        //public string CloseTimeFormatted { get; set; }

        ///// <summary>
        ///// Gets or sets the PauseStart.
        ///// </summary>
        //public string PauseStartFormatted { get; set; }

        ///// <summary>
        ///// Gets or sets the PauseEnd.
        ///// </summary>
        //public string PauseEndFormatted { get; set; }

        /// <summary>
        /// Gets or sets the PauseStart.
        /// </summary>
        public bool Close { get; set; }
    }
}
