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
    public class RatingMapper : EntityTypeConfiguration<Rating>
    {
        public RatingMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            this.Property(s => s.Description).IsRequired();
            this.Property(s => s.Description).IsMaxLength();
            this.Property(s => s.Description).IsUnicode(true);
            this.Property(s => s.State).IsRequired();            
            this.Property(s => s.Time).IsRequired();
                        
            //table  
            this.ToTable("dbo.Rating");

            //relationship              
            this.HasRequired(e => e.User).WithMany(e => e.RatingList).Map(s => s.MapKey("UserId")).WillCascadeOnDelete(true);
            this.HasRequired(e => e.Room).WithMany(e => e.RatingList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
