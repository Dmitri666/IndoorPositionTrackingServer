// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Table.cs" company="">
//   
// </copyright>
// <summary>
//   The table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel.RoomPlan
{
    using System;

    /// <summary>
    ///     The table.
    /// </summary>
    public class Table
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Table" /> class.
        /// </summary>
        public Table()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the angle.
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid ItemId { get; set; }

        public string ItemType { get; set; }

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        public double Top { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
        #endregion
    }
}