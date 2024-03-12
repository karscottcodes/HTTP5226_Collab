using System.Web;
using System.Web.Mvc;

namespace HTTP5226_Collab
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
