using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.DbMappers
{
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class BookingMapper : EntityTypeConfiguration<Booking>
    {
        public BookingMapper()
        {
            //this.HasRequired(e => e.Table).WithMany(e => e.Bookings).Map(s => s.MapKey("RoomTableId")).WillCascadeOnDelete(true);            
            this.HasRequired(e => e.User);
        }
    }
}
