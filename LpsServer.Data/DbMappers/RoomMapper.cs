namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class RoomMapper : EntityTypeConfiguration<Room>
    {
        public RoomMapper()
        {
            //Key
            this.HasKey(s => s.Id);

            //Fields  
            //this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();
            this.Property(s => s.Name).IsRequired();
            this.Property(s => s.Name).HasMaxLength(100);
            this.Property(s => s.Name).IsUnicode(true);
            this.Property(s => s.Time).IsRequired();
                       
            this.Property(s => s.Street).HasMaxLength(50);
            this.Property(s => s.StreetNumber).HasMaxLength(10);
            this.Property(s => s.City).HasMaxLength(20);
            this.Property(s => s.Plz).HasMaxLength(10);
            this.Property(s => s.Phone).HasMaxLength(50);
            this.Property(s => s.Email).HasMaxLength(50);
            this.Property(s => s.Homepage).HasMaxLength(50);
            this.Property(s => s.CanvasImage).IsUnicode(true);
            this.Property(s => s.CanvasImage).HasMaxLength(200);
            this.Property(s => s.Location).IsOptional();           
            this.Property(s => s.IsVisibleBusinessHours).IsRequired();
            this.Property(s => s.Description).IsMaxLength();
            this.Property(s => s.IsChatExist).IsRequired();

            //table  
            this.ToTable("dbo.Room");

            //relationship  
            this.HasRequired(e => e.User).WithMany(e => e.Rooms).Map(s => s.MapKey("UserId")).WillCascadeOnDelete(false);
        }
    }
}
