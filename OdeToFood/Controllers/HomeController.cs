using OdeToFood.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace OdeToFood.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		OdeToFoodDb _db = new OdeToFoodDb();

		public ActionResult AutoComplete(string term)
		{
			var model = _db.Restaurants
				.Where(r => r.Name.StartsWith(term))
				.Take(10)
				.Select(r => new
				{
					RestaurantName = r.Name
				});
			return Json(model, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Index(string searchTerm = null, int page = 1)
		{
			//var model = from r in _db.Restaurants
			//            orderby r.Reviews.Average(reviews => reviews.Rating) descending
			//            select new RestaurantListViewModel
			//            {
			//                Id = r.Id,
			//                Name = r.Name,
			//                City = r.City,
			//                Country = r.Country,
			//                CountOfReviews = r.Reviews.Count()
			//            };

			var model = _db.Restaurants
				.OrderByDescending(r => r.Reviews.Average(reviews => reviews.Rating))
				.Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
				.Select(r => new RestaurantListViewModel
				{
					Id = r.Id,
					Name = r.Name,
					City = r.City,
					Country = r.Country,
					CountOfReviews = r.Reviews.Count()
				}).ToPagedList(page, 10);

			if (Request.IsAjaxRequest())
			{
				return PartialView("_Restaurants", model);
			}

			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}