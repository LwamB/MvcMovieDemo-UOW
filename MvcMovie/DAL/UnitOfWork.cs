using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.DAL
{

    public class UnitOfWork : IUnitOfWork
    {
        private MovieContext _context;

        private IRepository<Movie> movieRepository;
        private IRepository<Rating> ratingRepository;
        public UnitOfWork(MovieContext context)
        {
            _context = context;
        }

        public IRepository<Movie> MovieRepository
        {
            get
            {
                if (movieRepository == null)
                {
                    movieRepository =
                        new GenericRepository<Movie>(_context);
                }
                return movieRepository;
            }
        }

        public IRepository<Rating> RatingRepository
        {
            get
            {
                if (ratingRepository == null)
                {
                    ratingRepository = new GenericRepository<Rating>(_context);
                }

                return ratingRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
