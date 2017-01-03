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
    public class LoggingMapper : EntityTypeConfiguration<Logging>
    {
        public LoggingMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();       
     
            this.Property(c => c.Message).IsUnicode(true);
            this.Property(c => c.InnerException).IsUnicode(true);
            this.Property(c => c.Data).IsUnicode(true);
            this.Property(c => c.Time).IsRequired();
            this.Property(c => c.InputData).IsUnicode(true);

            //table  
            this.ToTable("dbo.Logging");            
        }
    }
}
