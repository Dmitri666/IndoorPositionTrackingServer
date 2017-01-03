namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class FavoritsMapper : EntityTypeConfiguration<Favorits>
    {
        public FavoritsMapper()
        {
            //Key
            this.HasKey(s => s.Id);

            //Fields  
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            //table  
            this.ToTable("dbo.Favorits");

            //relationship  
            this.HasRequired(e => e.User).WithMany(e => e.FavoritsList).Map(s => s.MapKey("UserId")).WillCascadeOnDelete(true);
            this.HasRequired(e => e.Room).WithMany(e => e.FavoritsList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
