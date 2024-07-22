using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OcdlogisticsSolution.Web.Models
{
    public class JQueryDataTableParamModel
    {
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public string iColumns { get; set; }
        public string iSortingCols { get; set; }
        public string sColumns { get; set; }
        public string sortDirection { get; set; }
        public int sortColumnIndex { get; set; }
    }
}