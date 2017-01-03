using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.DbMappers
{
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class BookedTableMapper : EntityTypeConfiguration<BookedTable>
    {
        public BookedTableMapper()
        {
            this.HasRequired(e => e.Table).WithMany(e => e.Bookings).Map(s => s.MapKey("TableId")).WillCascadeOnDelete(true);
            this.HasRequired(e => e.Booking).WithMany(e => e.Tables).Map(s => s.MapKey("BookingId")).WillCascadeOnDelete(true);
        }
    }
}
