using Data.Context;
using Data.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Core.Infrastructure
{
    public static class Seeder
    {
        #region Methods
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Look for any Book.
                if (dbContext.Book.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateData(dbContext);
            }
        }

        public static void PopulateData(AppDbContext dbContext)
        {
            dbContext.Book.Add(new Book
            {
                Name = "Eloquent JavaScript, Second Edition",
                Description = "A Modern Introduction to Programming",
                Author = "Marijn Haverbeke",
                Price = 472.15M,
                StockQuantity = 5
            });
            dbContext.Book.Add(new Book
            {
                Name = "Learning JavaScript Design Patterns",
                Description = "A JavaScript and jQuery Developer's Guide",
                Author = "Addy Osmani",
                Price = 254.99M,
                StockQuantity = 10
            });
            dbContext.Book.Add(new Book
            {
                Name = "Speaking JavaScript",
                Description = "An In-Depth Guide for Programmers",
                Author = "Axel Rauschmayer",
                Price = 460.10M,
                StockQuantity = 15
            });
            dbContext.Book.Add(new Book
            {
                Name = "Programming JavaScript Applications",
                Description = "Robust Web Architecture with Node, HTML5, and Modern JS Libraries",
                Author = "Eric Elliott",
                Price = 254.49M,
                StockQuantity = 5
            });
            dbContext.Book.Add(new Book
            {
                Name = "Understanding ECMAScript 6",
                Description = "The Definitive Guide for JavaScript Developers",
                Author = "Nicholas C. Zakas",
                Price = 352.52M,
                StockQuantity = 10
            });
            dbContext.Book.Add(new Book
            {
                Name = "You Don't Know JS",
                Description = "ES6 & Beyond",
                Author = "Kyle Simpson",
                Price = 278.95M,
                StockQuantity = 15
            });
            dbContext.Book.Add(new Book
            {
                Name = "Git Pocket Guide",
                Description = "A Working Introduction",
                Author = "Richard E. Silverman",
                Price = 234.99M,
                StockQuantity = 5
            });
            dbContext.Book.Add(new Book
            {
                Name = "Designing Evolvable Web APIs with ASP.NET",
                Description = "Harnessing the Power of the Web",
                Author = "Glenn Block",
                Price = 538.09M,
                StockQuantity = 10
            });

            dbContext.SaveChanges();
        }
        #endregion
    }
}
