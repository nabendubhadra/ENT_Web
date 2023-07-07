using ENT_Web.Classes;
using ENT_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENT_Web.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        UserDataLayer dl = new UserDataLayer();
        public ActionResult Index(AdminModel.Publication publication, int? id)
        {
            id = Convert.ToInt32(RouteData.Values["id"]);
            publication.IssueId = Convert.ToInt64(id);

            DataSet ds = dl.GetPublication(publication);
            ViewBag.Publication = ds;

            return View();
        }

        public ActionResult ArticleDetails(AdminModel.Publication publication, int? id)
        {
            id = Convert.ToInt32(RouteData.Values["id"]);
            publication.PublicationId = Convert.ToInt64(id);

            DataSet ds = dl.GetPublication(publication);
            ViewBag.Publication = ds;

            return View();
        }
    }
}