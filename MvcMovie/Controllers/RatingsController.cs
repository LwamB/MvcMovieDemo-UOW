using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using MvcMovie.DAL;
using MvcMovie.Models;
using System.Data;

namespace MvcMovie.Controllers
{
    public class RatingsController : Controller
    {
        private IUnitOfWork _uow;

        public RatingsController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: Ratings/List
        public IActionResult List()
        {
            //ratingRepository.GetAll();
            //var ratings = ratingRepository.Ratings.OrderBy(r => r.Name);
            //ratingRepository.OrderBy(ratingRepository => ratingRepository.Name);

            return View(_uow.RatingRepository.GetAll());
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RatingID,Code,Name")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _uow.RatingRepository.Insert(rating);
                _uow.Save();
                return RedirectToAction("List");
            }
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public IActionResult Edit(int id)
        {
            //var rating = ratingRepository.GetByID(id);

            //return View(rating);
            return View(_uow.RatingRepository.GetByID(id));
        }

        // POST: Ratings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RatingID,Code,Name")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _uow.RatingRepository.Update(rating);
                    _uow.Save();
                }
                catch (DataException)
                {
                    throw;
                }
                return RedirectToAction("List");
            }
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public IActionResult Delete(int id)
        {
            //var rating = _context.Ratings.SingleOrDefault(r => r.RatingID == id);
            _uow.RatingRepository.Delete(id);
            _uow.Save();
            return RedirectToAction("List");
        }

    }
}
