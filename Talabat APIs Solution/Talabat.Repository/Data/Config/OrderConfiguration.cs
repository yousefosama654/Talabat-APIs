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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, np => np.WithOwner());
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.status).HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
