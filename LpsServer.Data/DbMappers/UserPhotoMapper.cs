namespace LpsServer.Data.DbMappers
{
    using System.Data.Entity.ModelConfiguration;
    using LpsServer.Data.Entities;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserPhotoMapper : EntityTypeConfiguration<UserPhoto>
    {
        public UserPhotoMapper()
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
            this.ToTable("dbo.UserPhoto");

            //relationship                          
            this.HasRequired(e => e.User).WithMany(e => e.PhotoList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
