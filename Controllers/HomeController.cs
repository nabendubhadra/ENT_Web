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
    public class HomeController : Controller
    {
        UserDataLayer dl = new UserDataLayer();
        public ActionResult Index(AdminModel.CMS cms, AdminModel.CMSType type, AdminModel.Issue issue, AdminModel.Publication publication)
        {
            DataSet ds = dl.GetCMS(cms);
            if (ds.Tables[0].Rows.Count > 0)
            {
                type.CMSTypeId = Convert.ToInt64(ds.Tables[0].Rows[0]["CMSTypeId"].ToString());
                ViewBag.CMS = ds;
            }
            
            DataSet ds2 = dl.GetIssue(issue);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ds2.Tables[0].DefaultView.Sort = "PublicationDate desc";
                
                ViewBag.Issue = ds2;
                publication.IssueId = Convert.ToInt64(ds2.Tables[0].Rows[0]["IssueId"].ToString());
                publication.PublicationId = 0;
            }
            
            DataSet ds3 = dl.GetPublication(publication);
            if(ds3.Tables[0].Rows.Count > 0)
            {
                ViewBag.Publication = ds3;
            }

            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewsLetter(ENTModel.NewsLetter news)
        {
            if (ModelState.IsValid)
            {
                news.CreationDate = DateTime.Today;
            }

            dl.SetNewsletter(news);
            return Redirect(Url.Action("Index", "Home"));
        }

       
    }
}