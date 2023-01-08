using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;
namespace Talabat.Repository.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    { 
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(o => o.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
