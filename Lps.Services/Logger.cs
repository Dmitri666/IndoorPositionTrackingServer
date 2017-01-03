// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="">
//   
// </copyright>
// <summary>
//   The logger.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Services
{
    using System;

    using LpsServer.Data;
    using LpsServer.Data.Entities;

    /// <summary>
    ///     The logger.
    /// </summary>
    public static class Logger
    {
        #region Public Methods and Operators

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public static void Log(Exception e, string inputData)
        {
            using (var context = new LpsContext())
            {
                var logging = new Logging
                {
                    Data = e.StackTrace,
                    InnerException =
                        e.InnerException != null ? e.InnerException.ToString() : string.Empty,
                    Message = e.Message,
                    Time = DateTime.Now,
                    InputData = inputData
                };

                context.LoggingData.Add(logging);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public static void Log(Exception e)
        {
            using (var context = new LpsContext())
            {
                var logging = new Logging
                {
                    Data = e.StackTrace, 
                    InnerException =
                        e.InnerException != null ? e.InnerException.ToString() : string.Empty, 
                    Message = e.Message,
                    Time = DateTime.Now
                };

                context.LoggingData.Add(logging);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// The log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void Log(string message)
        {
            using (var context = new LpsContext())
            {
                var logging = new Logging { Message = message, Time = DateTime.Now };

                context.LoggingData.Add(logging);
                context.SaveChanges();
            }
        }

        #endregion
    }
}