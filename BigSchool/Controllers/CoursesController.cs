using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Create()
        {
            //get list category
            BigSchoolDBContext con = new BigSchoolDBContext();
            Course objCourse = new Course();
            objCourse.ListCategory = con.Categories.ToList();
            return View(objCourse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objcourse)
        {
            BigSchoolDBContext con = new BigSchoolDBContext();

            ModelState.Remove("LecturerId"); 
            if (!ModelState.IsValid) 
            { 
                objcourse.ListCategory = con.Categories.ToList(); 
                return View("Create", objcourse); 
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objcourse.LecturerId = user.Id;

            con.Courses.Add(objcourse);
            con.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}