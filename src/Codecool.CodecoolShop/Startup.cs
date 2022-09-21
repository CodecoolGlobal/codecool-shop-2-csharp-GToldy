using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            var daoMemory = ProductDaoMemory.GetInstance();
            var categoryDao = ProductCategoryDaoMemory.GetInstance();
            services.AddSingleton<IProductDao>(daoMemory);
            services.AddSingleton<IProductCategoryDao>(categoryDao);
            
            services.AddSingleton<ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            // Suppliers
            Supplier amazon = new Supplier
            {
                Name = "Amazon",
                Description = "Digital content and services"
            };
            Supplier hasbro = new Supplier
            {
                Name = "Hasbro",
                Description = "Provides games to all ages."
            };

            supplierDataStore.Add(amazon);
            supplierDataStore.Add(hasbro);

            // Categories
            ProductCategory adult = new ProductCategory
            {
                Name = "Adult",
                Description = "A game played by adult players."
            };
            ProductCategory card = new ProductCategory
            {
                Name = "Card",
                Description = "A game mainly or only played using cards."
            };
            ProductCategory board = new ProductCategory
            {
                Name = "Board",
                Description = "A game including a board or mat, and moving parts."
            };

            productCategoryDataStore.Add(adult);
            productCategoryDataStore.Add(card);
            productCategoryDataStore.Add(board);

            // Products
            productDataStore.Add(new Product
            {
                Name = "Foreplay in a row",
                Image = "Foreplay in a row.jpg",
                Players = "2-?",
                DefaultPrice = 23.0m,
                Currency = "USD",
                Description = "Yup",
                ProductCategory = adult,
                Supplier = amazon
            });
            productDataStore.Add(new Product
            {
                Name = "Cluedo",
                Image = "Cluedo.jpg",
                Players = "2-?",
                DefaultPrice = 23.5m,
                Currency = "USD",
                Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.",
                ProductCategory = board,
                Supplier = hasbro
            });
            productDataStore.Add(new Product
            {
                Name = "Codenames",
                Image = "Codenames.jpg",
                Players = "2-?",
                DefaultPrice = 89.0m,
                Currency = "USD",
                Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.",
                ProductCategory = card,
                Supplier = amazon
            });
        }
    }
}
