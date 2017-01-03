// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPositionChangeTracker.cs" company="">
//   
// </copyright>
// <summary>
//   The PositionChangeTracker interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.Interfaces
{
    using System;
    using System.Collections.Generic;

    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Chat;

    /// <summary>
    /// The PositionChangeTracker interface.
    /// </summary>
    public interface IPositionChangeTracker
    {
        #region Public Events

        /// <summary>
        /// The position changed.
        /// </summary>
        event EventHandler<KeyValuePair<Guid, IList<DevicePosition>>> PositionChanged;

        #endregion
    }
}