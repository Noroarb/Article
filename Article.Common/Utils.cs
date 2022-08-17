using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Common
{
    public static class Utils
    {
       
        public static DateTime ServerNow
        {
            get
            {
                  return DateTime.Now.AddHours(10);
               // return DateTime.Now;
            }
        }

        public static string API_PATH = "http://noroarb2000-001-site1.htempurl.com/";
        

        public static string ArticleURL = API_PATH+ "/Articles/";
       
        public static string PhysicalArticle = "~/Articles/";


        public static string OldPath = "\\web\\";
       // public static string OldPath = "\\Article.Web\\";
        public static string NewPath = "\\";

        public static string ImageDefaultName = "index.png";

        

    }
    /// <summary>
    /// Article State
    /// </summary>
    public enum ArticleState { Draft = 0, Pending = 1, Accepted = 2, Rejected = 3 };
   
    
    public static class Roles
    {
        public static string AdminRole = "AdminRole";
        public static string SupervisorRole = "SupervisorRole";
        public static string WriterRole = "WriterRole";
        public static string UserRole = "UserRole";

    }
 

    
}