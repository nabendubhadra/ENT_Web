using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Classes
{
    public class UserDataLayerFunction
    {
        private connection cnn = new connection();

        protected DataSet Executeproc(string storedproc, string[] paraname, string[] paravalue)
        {
            try
            {
                SqlDataAdapter da;
                DataSet ds;
                SqlConnection cn = new SqlConnection(cnn.DbConnectionString);
                SqlCommand cmd = new SqlCommand(storedproc, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paraname.Length; i++)
                {
                    cmd.Parameters.AddWithValue(paraname[i], paravalue[i]);
                }
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                cn.Dispose();
                cn.Close();
                return ds;
            }
            catch
            {
                throw;
            }
        }

        protected int ExecuteNonproc(string storedproc, string[] paraname, string[] paravalue)
        {
            try
            {
                int result = 0;
                SqlConnection cn = new SqlConnection(cnn.DbConnectionString);
                SqlCommand cmd = new SqlCommand(storedproc, cn);

                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paraname.Length; i++)
                {
                    cmd.Parameters.AddWithValue(paraname[i], paravalue[i]);
                }
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();
                result = cmd.ExecuteNonQuery();
                cn.Dispose();
                cn.Close();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataSet Inline_Process(String Query)
        {
            SqlConnection con = new SqlConnection(cnn.DbConnectionString);
            SqlCommand cmd = new SqlCommand(Query, con);

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            con.Dispose();
            return ds;
        }

        public string SaveSingleImages(string directory, HttpPostedFileBase f)
        {
            string path = "", retpath = "";
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                }

                if (f != null)
                {
                    if (f.ContentLength > 0)
                    {
                        int fCount = 0;
                        fCount = Directory.GetFiles(HttpContext.Current.Server.MapPath(directory), "*", SearchOption.AllDirectories).Length;
                        fCount++;
                        path = directory + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "_" + fCount.ToString() + Path.GetExtension(f.FileName);

                        f.SaveAs(HttpContext.Current.Server.MapPath(path));
                        if (File.Exists(HttpContext.Current.Server.MapPath(path)))
                        {
                            retpath = path;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return retpath;
        }

        //--------------------------------------------------------------------

        public string NewSaveSingleImages(string directory, HttpPostedFileBase f, string oldfile)
        {
            string path = "", retpath = "";
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                {

                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                }
                if (f != null)
                {
                    try
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath(directory + oldfile)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(directory + oldfile));
                        }
                    }
                    catch (Exception)
                    {
                    }

                    if (f.ContentLength > 0)
                    {
                        int fCount = 0;
                        fCount =
                            Directory.GetFiles(HttpContext.Current.Server.MapPath(directory), "*",
                                SearchOption.AllDirectories).Length;
                        fCount++;
                        oldfile = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + "_" + fCount.ToString() +
                                  Path.GetExtension(f.FileName);
                        path = directory + oldfile;

                        f.SaveAs(HttpContext.Current.Server.MapPath(path));
                        if (File.Exists(HttpContext.Current.Server.MapPath(path)))
                        {
                            retpath = oldfile;
                        }
                    }
                }
                else
                {
                    if (oldfile != "")
                    {
                        path = directory + oldfile;
                        if (File.Exists(HttpContext.Current.Server.MapPath(path)))
                        {
                            retpath = oldfile;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return retpath;
        }

        public bool CheckImageExtention(string ext)
        {
            bool flag = false;
            string[] extensionlist = { "jpg", "jpeg", "bmp", "png" };
            for (int i = 0; i < extensionlist.Length; i++)
            {
                if (ext.ToString().ToLower() == extensionlist[i])
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool CheckResumeExtention(string ext)
        {
            bool resume = false;
            string[] extensionlist = { "txt", "pdf", "doc", "docx" };
            for (int i = 0; i < extensionlist.Length; i++)
            {
                if (ext.ToString().ToLower() == extensionlist[i])
                {
                    resume = true;
                }
            }
            return resume;
        }


    }
}