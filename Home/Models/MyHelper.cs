using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home.Models
{
    public static class MyHelper
    {
        public static IHtmlString LabelWithMark(string content)
        {
            string htmlString = String.Format("<label><mark>{0}</mark></label>");
            return new HtmlString(htmlString);
        }
    }
}