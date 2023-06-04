using MvcMovie.Models;
using System;
using System.Linq;

namespace MvcMovie.Data
{
    public class DBInitializer
    {
        public static void Initialize(MovieContext context)
        {
            context.Database.EnsureCreated();

            // Look for any movies.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }
            
            context.Ratings.AddRange(
                new Rating { Code = "G", Name = "General" },
                new Rating { Code = "PG", Name = "Parental Guidance" },
                new Rating { Code = "M", Name = "Mature" });
            context.SaveChanges();

            context.Movies.AddRange(
                     new Movie
                     {
                         Title = "When Harry Met Sally",
                         ReleaseDate = DateTime.Parse("1989-1-11"),
                         Genre = "Romantic Comedy",
                         Price = 8,
                         RatingID = 1
                     },
                     new Movie
                     {
                         Title = "Ghostbusters",
                         ReleaseDate = DateTime.Parse("1984-3-13"),
                         Genre = "Comedy",
                         Price = 9,
                         RatingID = 1
                     },
                     new Movie
                     {
                         Title = "Ghostbusters 2",
                         ReleaseDate = DateTime.Parse("1986-2-23"),
                         Genre = "Comedy",
                         Price = 10,
                         RatingID = 1
                     },
                     new Movie
                     {
                         Title = "Rio Bravo",
                         ReleaseDate = DateTime.Parse("1959-4-15"),
                         Genre = "Western",
                         Price = 4,
                         RatingID = 2
                     });
            context.SaveChanges();
        }
    }
}
