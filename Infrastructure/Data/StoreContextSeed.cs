using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!storeContext.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        storeContext.ProductBrands.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

                if (!storeContext.ProductTypes.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var brands = JsonSerializer.Deserialize<List<ProductType>>(brandsData);

                    foreach (var item in brands)
                    {
                        storeContext.ProductTypes.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

                if (!storeContext.Products.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var brands = JsonSerializer.Deserialize<List<Product>>(brandsData);

                    foreach (var item in brands)
                    {
                        storeContext.Products.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

                if (!storeContext.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        storeContext.DeliveryMethods.Add(item);
                    }

                    await storeContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "Error while seeding data.");
            }
        }
    }
}