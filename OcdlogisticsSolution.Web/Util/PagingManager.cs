using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OcdlogisticsSolution.Web.Util
{
    public class PagingManager
    {
        public static int GetPageSize()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }
    }
}