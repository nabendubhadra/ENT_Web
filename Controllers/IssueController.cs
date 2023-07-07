using ENT_Web.Classes;
using ENT_Web.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENT_Web.Controllers
{
    public class IssueController : Controller
    {
        // GET: Issue

        UserDataLayer dl = new UserDataLayer();
        public ActionResult Index(AdminModel.Issue issue, AdminModel.Publication publication)
        {
            DataSet ds = dl.GetIssue(issue);
            ds.Tables[0].DefaultView.Sort = "PublicationDate desc";
            ViewBag.Issue = ds;

            publication.IssueId = 1;
            DataSet ds1 = dl.GetPublication(publication);
            ViewBag.Publication = ds1;

            return View();
        }
    }
}