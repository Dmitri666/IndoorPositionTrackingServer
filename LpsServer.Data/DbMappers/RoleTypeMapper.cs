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
    public class RoleTypeMapper : EntityTypeConfiguration<RoleType>
    {
        public RoleTypeMapper()
        {
            //Key
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            this.Property(s => s.Order).IsRequired();
            this.Property(s => s.Name).IsRequired();
            this.Property(s => s.Name).HasMaxLength(100);
            this.Property(s => s.Name).IsUnicode(true);
            this.Property(s => s.Description).IsRequired();
            this.Property(s => s.Description).HasMaxLength(200);
            this.Property(s => s.Description).IsUnicode(true);
            

            //table  
            this.ToTable("dbo.RoleType");                       
        }
    }
}
