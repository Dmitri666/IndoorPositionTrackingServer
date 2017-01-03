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
    public class RoomKitchenInternationalMapper : EntityTypeConfiguration<RoomKitchenInternational>
    {
        public RoomKitchenInternationalMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
                        
            //table  
            this.ToTable("dbo.RoomKitchenInternational");

            //relationship              
            this.HasRequired(e => e.KitchenInternationalType).WithMany(e => e.RoomKitchenInternationalList).Map(s => s.MapKey("KitchenInternationalTypeId")).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Room).WithMany(e => e.RoomKitchenInternationalList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
