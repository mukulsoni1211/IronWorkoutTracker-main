using IronWorkoutTracker.Presentation.PresentationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
   [Authorize(Policy = PolicyNameConstants.AdminOnly)]
    public class ExcerciseController : Controller
    {
        // GET: ExcerciseControler
        public ActionResult Index()
        {
            return Content("This is the page to manage Excercises and Workouts");
        }

    }
}
