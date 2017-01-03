namespace LpsServer.Data.DbMappers
{
    using LpsServer.Data.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class BookingRoomTableMapper : EntityTypeConfiguration<BookingRoomTable>
    {
        public BookingRoomTableMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
                        
            //table  
            this.ToTable("dbo.BookingRoomTable");

            //relationship              
            this.HasRequired(e => e.Booking).WithMany(e => e.Tables).Map(s => s.MapKey("BookingId")).WillCascadeOnDelete(true);
            this.HasRequired(e => e.RoomTable).WithMany(e => e.Tables).Map(s => s.MapKey("RoomTableId")).WillCascadeOnDelete(false);
        }
    }
}
