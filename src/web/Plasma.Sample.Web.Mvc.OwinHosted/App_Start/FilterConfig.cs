using System.Web;
using System.Web.Mvc;

namespace Plasma.Sample.Web.Mvc.OwinHosted
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
