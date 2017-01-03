using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.DbMappers
{
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class RoomTableMapper : EntityTypeConfiguration<RoomTable>
    {
        public RoomTableMapper()
        {
            this.HasRequired(e => e.Room).WithMany(e => e.Tables).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
