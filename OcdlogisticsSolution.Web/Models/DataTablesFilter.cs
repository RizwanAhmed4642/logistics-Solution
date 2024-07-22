using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OcdlogisticsSolution.Web.Models
{
    public class DataTablesFilter
    {
        public static JQueryDataTableParamModel SetDataTablesFilter(HttpRequestBase request)
        {
            var urlParameters = HttpUtility.ParseQueryString(request.Url.Query);
            JQueryDataTableParamModel jqObj = new JQueryDataTableParamModel();
            jqObj.sEcho = urlParameters.Get("sEcho");
            jqObj.sSearch = urlParameters.Get("sSearch");
            jqObj.iDisplayLength = Convert.ToInt32(urlParameters.Get("iDisplayLength"));
            jqObj.iDisplayStart = Convert.ToInt32(urlParameters.Get("iDisplayStart"));
            jqObj.iSortingCols = urlParameters.Get("iSortingCols");
            jqObj.iColumns = urlParameters.Get("iColumns");
            jqObj.sortDirection = urlParameters.Get("sSortDir_0");
            jqObj.sortColumnIndex = Convert.ToInt32(urlParameters.Get("iSortCol_0"));
            return jqObj;
        }
    }
}