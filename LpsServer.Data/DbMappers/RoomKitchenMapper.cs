using LpsServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.DbMappers
{    
    public class RoomKitchenMapper : EntityTypeConfiguration<RoomKitchen>
    {
        public RoomKitchenMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
                        
            //table  
            this.ToTable("dbo.RoomKitchen");

            //relationship              
            this.HasRequired(e => e.KitchenType).WithMany(e => e.KitchenTypeList).Map(s => s.MapKey("KitchenTypeId")).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Room).WithMany(e => e.RoomKitchenList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
