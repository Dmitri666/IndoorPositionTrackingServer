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
    public class KitchenMenuMapper : EntityTypeConfiguration<KitchenMenu>
    {
        public KitchenMenuMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            this.Property(s => s.Name).IsRequired();
            this.Property(s => s.Name).HasMaxLength(100);
            this.Property(s => s.Name).IsUnicode(true);
            this.Property(s => s.Price).IsRequired();
            this.Property(s => s.Description).HasMaxLength(300);
            this.Property(s => s.Description).IsUnicode(true);
            this.Property(s => s.Order).IsRequired();
                        
            //table  
            this.ToTable("dbo.KitchenMenu");

            //relationship              
            this.HasRequired(e => e.KitchenMenuType).WithMany(e => e.KitchenMenuList).Map(s => s.MapKey("KitchenMenuTypeId")).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Room).WithMany(e => e.KitchenMenuList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
