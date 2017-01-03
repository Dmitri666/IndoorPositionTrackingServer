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
    public class SpecializationMapper : EntityTypeConfiguration<Specialization>
    {
        public SpecializationMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
                        
            //table  
            this.ToTable("dbo.Specialization");

            //relationship              
            this.HasRequired(e => e.SpecializationType).WithMany(e => e.SpecializationList).Map(s => s.MapKey("SpecializationTypeId")).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Room).WithMany(e => e.SpecializationList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
