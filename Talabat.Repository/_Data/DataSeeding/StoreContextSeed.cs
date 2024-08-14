using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;
using Talabat.Repository._Data.DataContext;

namespace Talabat.Repository._Data.DataSeeding
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {

            if (context.productBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (context.productCategories.Count() == 0)
            {
                var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

                if (categories?.Count > 0)
                {
                    foreach (var cateogry in categories)
                    {
                        context.Set<ProductCategory>().Add(cateogry);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (context.products.Count() == 0)
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Products>>(productData);

                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        context.Set<Products>().Add(product);
                    }
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
