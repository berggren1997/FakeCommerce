using FakeCommerce.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FakeCommerce.DataAccess.Data
{
    public static class DbInitializer
    {
        public static void SeedData(this IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<CommerceDbContext>();
            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category
                    {
                        Name = "Shirt"
                    },
                    new Category
                    {
                        Name = "Pants"
                    },
                    new Category
                    {
                        Name = "Blouse"
                    },
                });
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>
                {
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f1.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f1_zm8myl.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f2.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f2_k3obbo.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f3.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f3_qkjeiy.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f4.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f4_qcxs37.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f5.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f5_qojghp.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Manchester Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f6.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f6_teh5ws.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Gray Pants",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f7.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/f7_buoar9.jpg",
                        CategoryId = 2,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Pink Blouse",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/f8.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410800/f8_jfwrpt.jpg",
                        CategoryId = 3,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Long-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n1.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/n1_kjxicq.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Long-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n2.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/n2_wq8cy2.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Long-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n3.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410799/n3_jqwozs.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n4.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410800/n4_dv6tsz.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Long-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n5.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410800/n5_jokycy.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Shorts",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n6.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410800/n6_xfvisv.jpg",
                        CategoryId = 2,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Long-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n7.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410800/n7_mitxvw.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    },
                    new Product
                    {
                        Name = "Short-Sleeved Shirt",
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                        Price = 20,
                        //PictureUrl = "/img/products/n8.jpg",
                        PictureUrl = "https://res.cloudinary.com/dejvy5075/image/upload/v1672410800/n8_oidujg.jpg",
                        CategoryId = 1,
                        QuantityInStock = 100
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
