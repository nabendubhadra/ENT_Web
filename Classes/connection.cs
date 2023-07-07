using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


    public class connection
    {
    private string constr;
    public connection()
    {
        constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
    }
    public string DbConnectionString
    {
        get
        {
            return constr;
        }
    }
}
