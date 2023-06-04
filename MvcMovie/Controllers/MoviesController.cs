using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.DAL;
using MvcMovie.Models;
//using Microsoft.EntityFrameworkCore;
using MvcMovie.Models.ViewModels;
using System.Data;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private IUnitOfWork unitOfWork;
        public MoviesController(IUnitOfWork unitOfWork, IUnitOfWork _uow)
        {
            this.unitOfWork = unitOfWork;

        }

        public IActionResult List(int ratingID = 0)
        {
            var listMoviesVM = new ListMoviesViewModel();

            if (ratingID != 0)
            {
                listMoviesVM.Movies = unitOfWork.MovieRepository.Get(
                    filter: m => m.RatingID == ratingID,
                    orderBy: m => m.OrderBy(m => m.Title)).ToList();


            }
            else
            {
                listMoviesVM.Movies = unitOfWork.MovieRepository.Get(
                    orderBy: m => m.OrderBy(x => x.Title)).ToList();
            }

            listMoviesVM.Ratings =
                new SelectList(unitOfWork.RatingRepository.Get(

                    orderBy: m => m.OrderBy(x => x.Name)).ToList(),
                    "RatingID", "Name");


            listMoviesVM.ratingID = ratingID;

            return View(listMoviesVM);
        }

        public IActionResult Details(int id)
        {
            //var movie = _context.Movies
            //.Include(m => m.Rating)
            //.SingleOrDefault(m => m.MovieID == id);
            var movie = unitOfWork.MovieRepository.Get(
                filter: x => x.MovieID == id,
                includes: x => x.Rating).FirstOrDefault();

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["Ratings"] =
                new SelectList(unitOfWork.RatingRepository.GetAll().OrderBy(r => r.Name),
                               "RatingID",
                               "Name");

            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RatingID,Name")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RatingRepository.Insert(rating);
                unitOfWork.Save();
                return RedirectToAction("List");
            }
            return View(rating);
        }

        public IActionResult Add()
        {
            var addMovieVM = new AddMovieViewModel();
            addMovieVM.Movie = new Movie();
            return View(addMovieVM);
        }

        //POST: Movies/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Movie,Code, Name")] AddMovieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = unitOfWork.RatingRepository.Get
                        (filter: r => r.Code == model.Code && r.Name == model.Name).FirstOrDefault();

                    if (rating == null)
                    {
                        rating = new Rating { Code = model.Code, Name = model.Name };
                        unitOfWork.RatingRepository.Insert(rating);
                    }

                    model.Movie.Rating = rating;

                    unitOfWork.MovieRepository.Insert(model.Movie);
                    unitOfWork.Save();

                    return RedirectToAction("List");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return RedirectToAction("Add");
        }

    }
}