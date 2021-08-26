using books.Context;
using books.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books.Context
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "A Beautiful Mind",
                            Description = "Nasar thoroughly explores Nash’s prestigious career, from his beginnings at MIT to his work at the RAND Corporation",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            Rate = 4,
                            Genre = "Biography",
                            CoverUrl = "https://m.media-amazon.com/images/I/511wQDs0J7L.jpg",
                            DateAdded = DateTime.Now                            
                        },
                        new Book()
                        {
                            Title = "Quiet: The Power of Introverts in a World That Can't Stop Talking",
                            Description = "Nasar thoroughly explores Nash’s prestigious career, from his beginnings at MIT to his work at the RAND Corporation",
                            IsRead = false,
                            Genre = "Behavioral Psychology",
                            CoverUrl = "https://m.media-amazon.com/images/P/0307352153.01._SCLZZZZZZZ_SX500_.jpg",
                            DateAdded = DateTime.Now
                        });

                    context.SaveChanges();
                }
            }
        }
    }
}
