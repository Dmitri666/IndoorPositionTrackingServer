// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BeaconMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The beacon mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    /// <summary>
    /// The beacon mapper.
    /// </summary>
    public class BeaconMapper : EntityTypeConfiguration<Beacon>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BeaconMapper"/> class.
        /// </summary>
        public BeaconMapper()
        {
            this.HasRequired(e => e.Room)
                .WithMany(e => e.BeaconList)
                .Map(s => s.MapKey("Room_Id"))
                .WillCascadeOnDelete(true);

           
        }

        #endregion
    }
}