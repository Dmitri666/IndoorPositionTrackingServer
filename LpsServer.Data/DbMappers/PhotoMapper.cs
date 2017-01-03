namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class PhotoMapper : EntityTypeConfiguration<Photo>
    {
        public PhotoMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            //this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            
            this.Property(c => c.Time).IsRequired();
            this.Property(c => c.IsMain).IsRequired();            
            this.Property(c => c.Image).IsRequired();
            this.Property(s => s.Image).IsUnicode(true);
            this.Property(s => s.Image).HasMaxLength(200);

            //table  
            this.ToTable("dbo.Photo");

            //relationship                          
            this.HasRequired(e => e.Room).WithMany(e => e.PhotoList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
