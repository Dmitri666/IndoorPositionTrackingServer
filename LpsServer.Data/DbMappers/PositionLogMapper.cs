using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class PositionLogMapper : EntityTypeConfiguration<PositionLog>
    {
        public PositionLogMapper()
        {
            // Key 
            this.HasKey(c => c.Id);

            // Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            
        }

        
    }
}
