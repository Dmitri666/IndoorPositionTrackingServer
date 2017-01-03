// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The position mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    /// <summary>
    /// The position mapper.
    /// </summary>
    public class PositionMapper : EntityTypeConfiguration<Position>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionMapper"/> class.
        /// </summary>
        public PositionMapper()
        {
            // Key 
            this.HasKey(c => c.Id);

            // Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            this.Property(c => c.Time).IsRequired();
            this.Property(c => c.X).IsRequired();
            this.Property(c => c.Y).IsRequired();
            

            // table  
            this.ToTable("dbo.Position");

            // relationship              
            this.HasRequired(e => e.Device)
                .WithMany(e => e.PositiontList)
                .Map(s => s.MapKey("DeviceId"))
                .WillCascadeOnDelete(true);
            this.HasRequired(e => e.Room)
                .WithMany(e => e.PositionList)
                .Map(s => s.MapKey("RoomId"))
                .WillCascadeOnDelete(true);
        }

        #endregion
    }
}