using MvcMovie.Models;

namespace MvcMovie.DAL
{
    public interface IUnitOfWork
    {
        IRepository<Movie> MovieRepository { get; }
        IRepository<Rating> RatingRepository { get; }

        void Save();
    }
}
