using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(ILoggerFactory Ilogger, StoreContext context)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Talabat.Repository/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        await context.ProductBrands.AddAsync(brand);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = Ilogger.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
            try
            {
                if (!context.ProductTypes.Any())
                {
                    var typessData = File.ReadAllText("../Talabat.Repository/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typessData);
                    foreach (var type in types)
                    {
                        await context.ProductTypes.AddAsync(type);
                    }
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var logger = Ilogger.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
            try
            {
                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Talabat.Repository/DataSeed/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    foreach (var Product in Products)
                    {
                        await context.Products.AddAsync(Product);
                    }
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var logger = Ilogger.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
            try
            {
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/DataSeed/delivery.json");
                    var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    foreach (var DM in Delivery)
                    {
                        await context.DeliveryMethods.AddAsync(DM);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = Ilogger.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }

    }
}
