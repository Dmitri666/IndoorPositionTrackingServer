using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookedTable")]
    public class BookedTable
    {
        public virtual Booking Booking { get; set; }
        public virtual RoomTable Table { get; set; }
    }
}
