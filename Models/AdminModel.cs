using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ENT_Web.Models
{
    public class AdminModel
    {
        public class Issue
        {
            public Int64 IssueId { get; set; }
            public string IssueHeading { get; set; }
            public int IssuePosition { get; set; }
            public string PublicationNumber { get; set; }

            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? PublicationDate { get; set; }
            public string ImageFile { get; set; }
        }

        public class Publication
        {
            public Int64 PublicationId { get; set; }
            public Int64 IssueId { get; set; }
            public List<Publication> IssueHeadingList { get; set; }
            public string IssueHeading { get; set; }
            public string ArticleName { get; set; }
            public int ArticleNumber { get; set; }
            public string ArticleImage { get; set; }
            public string Details { get; set; }
            public string PDFFile { get; set; }
        }

        public class CMSType
        {
            public Int64 CMSTypeId { get; set; }
            public string CMS_Type { get; set; }
        }

        public class CMS
        {
            public Int64 CMSId { get; set; }
            public Int64 CMSTypeId { get; set; }
            public string CMSType { get; set; }
            public string Details { get; set; }
            public string Link { get; set; }
            public string ImageFile { get; set; }
            public List<CMS> CMSTypelist { get; set; }
        }
    }
}