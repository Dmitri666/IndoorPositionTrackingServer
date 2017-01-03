using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Services.Helper
{
    /// <summary>
    /// StaticData class
    /// </summary>
    public class StaticData
    {
        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public static string Version
        {
            get
            {
                return SystemVersion ?? (SystemVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the system version.
        /// </summary>
        /// <value>
        /// The system version.
        /// </value>
        private static string SystemVersion { get; set; }
    }
}
