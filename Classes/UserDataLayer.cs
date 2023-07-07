using Classes;
using ENT_Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;

namespace ENT_Web.Classes
{
    public class UserDataLayer: UserDataLayerFunction
    {
        public int SetIssue(AdminModel.Issue issue)
        {
            try
            {                

                string[] paraname = { "@IssueId","@IssueHeading", "@IssuePosition", "@PublicationNumber", "@PublicationDate", "@ImageFile" };
                string[] paravalue = { issue.IssueId.ToString(), issue.IssueHeading, issue.IssuePosition.ToString(), issue.PublicationNumber, issue.PublicationDate.ToString(), issue.ImageFile };

                return ExecuteNonproc("[MTR].[usp_setIssue]", paraname, paravalue);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetIssue(AdminModel.Issue issue)
        {
            string[] paraname = { "@IssueId" };
            string[] paravalue = { issue.IssueId.ToString() };

            DataSet ds = new DataSet();
            ds = Executeproc("[MTR].[usp_getIssue]", paraname, paravalue);
            return ds;
        }

        public int SetPublication(AdminModel.Publication publication)
        {
            try
            {
                string[] paraname = { "@PublicationId", "@IssueId", "@ArticleName", "@ArticleNumber","@ArticleImage", "@Details", "@PDFFile" };
                string[] paravalue = { publication.PublicationId.ToString(), publication.IssueId.ToString(), publication.ArticleName, publication.ArticleNumber.ToString(), publication.ArticleImage, publication.Details, publication.PDFFile };

                return ExecuteNonproc("[MTR].[usp_setPublication]", paraname, paravalue);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetPublication(AdminModel.Publication publication)
        {
            string[] paraname = { "@PublicationId", "@IssueId" };
            string[] paravalue = { publication.PublicationId.ToString(), publication.IssueId.ToString() };

            DataSet ds = new DataSet();
            ds = Executeproc("[MTR].[usp_getPublication]", paraname, paravalue);
            return ds;
        }

        public DataSet GetCMSType(AdminModel.CMSType cmsType)
        {

            string[] paraname = { "@CMSTypeId" };
            string[] paravalue = { cmsType.CMSTypeId.ToString() };

            DataSet ds = new DataSet();
            ds = Executeproc("[MTR].[usp_getCMSType]", paraname, paravalue);
            return ds;
        }

       public int SetCMS(AdminModel.CMS cms)
        {
            try
            {
                string[] paraname = { "@CMSId", "@CMSTypeId", "@Details", "@Link", "@ImageFile" };
                string[] paravalue = { cms.CMSId.ToString(), cms.CMSTypeId.ToString(), cms.Details, cms.Link, cms.ImageFile };

                return ExecuteNonproc("[MTR].[usp_setCMS]", paraname, paravalue);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCMS(AdminModel.CMS cms)
        {
            string[] paraname = { "@CMSId", "@CMSTypeId" };
            string[] paravalue = { cms.CMSId.ToString(), cms.CMSTypeId.ToString() };

            DataSet ds = new DataSet();
            ds = Executeproc("[MTR].[usp_getCMS]", paraname, paravalue);
            return ds;
        }

        public int SetNewsletter(ENTModel.NewsLetter news)
        {
            try
            {
                string[] paraname = { "@EmailId", "@CreationDate" };
                string[] paravalue = { news.EmailId.ToString(), news.CreationDate.ToString() };

                return ExecuteNonproc("[MTR].[usp_setNewsLetter]", paraname, paravalue);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}