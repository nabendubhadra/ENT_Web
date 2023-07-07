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
    public class AdminController : Controller
    {
        UserDataLayer dl = new UserDataLayer();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if (form["User"] == "admin" && form["Pass"] == "admin")
            {
                HttpCookie loginCookie = Request.Cookies["LoginCookie"];
                if (loginCookie != null)
                {
                    loginCookie.Expires = DateTime.Now.AddDays(-1);
                }
                loginCookie = new HttpCookie("LoginCookie");
                loginCookie["UserName"] = "admin";
                loginCookie["UserId"] = "admin";               
                loginCookie.Expires = DateTime.Now.AddMinutes(20);
                Response.Cookies.Add(loginCookie);
                return Redirect(Url.Action("Dashboard", "Admin"));
            }

            else
            {
                ViewBag.Msg = "Invalid Login Details";
            }

            return View();
        }

        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult Issue(AdminModel.Issue issue, int? id)
        {
            id = Convert.ToInt32(RouteData.Values["id"]);

            if (id != 0)
            {
                issue.IssueId = Convert.ToInt64(id);
                DataSet ds = dl.GetIssue(issue);
                int i = Convert.ToInt32(id);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    issue.IssueHeading = ds.Tables[0].Rows[0]["IssueHeading"].ToString();
                    issue.IssuePosition = Convert.ToInt32(ds.Tables[0].Rows[0]["IssuePosition"].ToString());
                    issue.PublicationNumber = ds.Tables[0].Rows[0]["PublicationNumber"].ToString();
                    issue.PublicationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PublicationDate"].ToString());
                    issue.ImageFile = ds.Tables[0].Rows[0]["ImageFile"].ToString();
                    ViewBag.Image = issue.ImageFile;
                }
                else
                {

                }
            }
            else
            { ViewBag.Image = "no-picture.png"; }

            return View(issue);
        }

        [HttpPost]
        public ActionResult Issue(AdminModel.Issue issue, HttpPostedFileBase ImageData)
        {
            int? id = Convert.ToInt32(RouteData.Values["id"]);

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (ImageData != null)
                {
                    issue.ImageFile = dl.NewSaveSingleImages("~/images/gallery/", ImageData, issue.ImageFile);
                }
                issue.IssueId = Convert.ToInt64(id);
                dl.SetIssue(issue);
                return Redirect(Url.Action("IssueList", "Admin"));
            }

            return View();
        }

        public ActionResult IssueList(AdminModel.Issue issue)
        {
            DataSet ds = dl.GetIssue(issue);
            ViewBag.Issue = ds;

            return View();
        }
        public ActionResult Publication(AdminModel.Issue issue, AdminModel.Publication publication, int? id)
        {
            id = Convert.ToInt32(RouteData.Values["id"]);
            DataSet ds = dl.GetIssue(issue);
            List<AdminModel.Publication> IssueHeader = new List<AdminModel.Publication>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdminModel.Publication publication1 = new AdminModel.Publication();
                    publication1.IssueId = Convert.ToInt64(ds.Tables[0].Rows[i]["IssueId"].ToString());
                    publication1.IssueHeading = ds.Tables[0].Rows[i]["IssueHeading"].ToString();
                    IssueHeader.Add(publication1);
                }

                publication.IssueHeadingList = IssueHeader;
            }

            if (id != 0)
            {
                publication.PublicationId = Convert.ToInt64(id);
                DataSet ds1 = dl.GetPublication(publication);
                int i = Convert.ToInt32(id);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    publication.IssueId = Convert.ToInt64(ds1.Tables[0].Rows[0]["IssueId"].ToString());
                    publication.ArticleName = ds1.Tables[0].Rows[0]["ArticleName"].ToString();
                    publication.ArticleNumber = Convert.ToInt32(ds1.Tables[0].Rows[0]["ArticleNumber"].ToString());
                    publication.ArticleImage = ds1.Tables[0].Rows[0]["ArticleImage"].ToString();
                    publication.Details = ds1.Tables[0].Rows[0]["Details"].ToString();
                    publication.PDFFile = ds1.Tables[0].Rows[0]["PDFFile"].ToString();
                    ViewBag.Image = publication.ArticleImage;
                }
            }
            else
            { ViewBag.Image = "no-picture.png"; }

            return View(publication);
        }

        [HttpPost]
        public ActionResult Publication(AdminModel.Publication publication, HttpPostedFileBase ImageData, HttpPostedFileBase DocData)
        {
            int? id = Convert.ToInt32(RouteData.Values["id"]);

            if (ModelState.IsValid)
            {
                if (ImageData != null)
                {
                    publication.ArticleImage = dl.NewSaveSingleImages("~/images/gallery/", ImageData, publication.ArticleImage);
                }

                if (DocData != null)
                {
                    publication.PDFFile = dl.NewSaveSingleImages("~/images/gallery/", DocData, publication.PDFFile);
                }

                publication.PublicationId = Convert.ToInt64(id);
                dl.SetPublication(publication);
                return Redirect(Url.Action("PublicationList", "Admin"));
            }

            return View();
        }
        public ActionResult PublicationList(AdminModel.Publication publication)
        {
            DataSet ds = dl.GetPublication(publication);
            ViewBag.Publication = ds;

            return View();
        }
        public ActionResult CMS(AdminModel.CMSType cmsType, AdminModel.CMS cms, int? id)
        {
            id = Convert.ToInt32(RouteData.Values["id"]);
            DataSet ds = dl.GetCMSType(cmsType);
            List<AdminModel.CMS> CMSType = new List<AdminModel.CMS>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdminModel.CMS cMS = new AdminModel.CMS();
                    cMS.CMSTypeId = Convert.ToInt64(ds.Tables[0].Rows[i]["CMSTypeId"].ToString());
                    cMS.CMSType = ds.Tables[0].Rows[i]["CMSType"].ToString();
                    CMSType.Add(cMS);
                }

                cms.CMSTypelist = CMSType;
            }

            if (id != 0)
            {
                cms.CMSId = Convert.ToInt64(id);
                DataSet ds1 = dl.GetCMS(cms);
                int i = Convert.ToInt32(id);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    cms.CMSTypeId = Convert.ToInt64(ds.Tables[0].Rows[0]["CMSTypeId"].ToString());
                    cms.CMSType = ds1.Tables[0].Rows[0]["CMSType"].ToString();
                    cms.ImageFile = ds1.Tables[0].Rows[0]["ImageFile"].ToString();
                    cms.Link = ds1.Tables[0].Rows[0]["Link"].ToString();
                    cms.Details = ds1.Tables[0].Rows[0]["Details"].ToString();
                    if (cms.ImageFile !="")
                    {
                        ViewBag.Image = cms.ImageFile;

                    }
                    else
                    {
                        ViewBag.Image = "no-picture.png";
                    }
                }
            }
            else
            { ViewBag.Image = "no-picture.png"; }
            return View(cms);
        }

        [HttpPost]
        public ActionResult CMS(AdminModel.CMS cms, HttpPostedFileBase ImageData)
        {
            int? id = Convert.ToInt32(RouteData.Values["id"]);

            if (ModelState.IsValid)
            {
                if (ImageData != null)
                {
                    cms.ImageFile = dl.NewSaveSingleImages("~/images/gallery/", ImageData, cms.ImageFile);
                }

                if (cms.Link == null)
                {
                    cms.Link = "";
                }

                cms.CMSId = Convert.ToInt64(id);
                dl.SetCMS(cms);
                return Redirect(Url.Action("CMSList", "Admin"));
            }

            return View();
        }
        public ActionResult CMSList(AdminModel.CMS cms)
        {
            DataSet ds = dl.GetCMS(cms);
            ViewBag.CMS = ds;

            return View();
        }

       
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            HttpCookie loginCookie = Request.Cookies["LoginCookie"];
            if (loginCookie != null)
            {
                loginCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(loginCookie);
            }
            HttpCookie lockingCookie = Request.Cookies["LoginCookie"];
            if (lockingCookie != null)
            {
                lockingCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(lockingCookie);
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Index", "Admin");
        }

    }
}