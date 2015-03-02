using System;
using Plasma.Core;

namespace Plasma.HttpClient
{
    public static class PlasmaClient
    {
		public static System.Net.Http.HttpClient For(IRequestProcessor application)
        {
            return BuildClient(application);
        }

        public static System.Net.Http.HttpClient For(string physicalPath, string virtualPath = "/", Action<IRequestProcessor> configure = null)
        {
	        var app = new AspNetApplication(virtualPath, physicalPath);
	        configure = configure ?? (a => { });
	        configure(app);
			return BuildClient(app);
        }

        public static System.Net.Http.HttpClient For<TApplicationType>(Action<IRequestProcessor> configure = null)
        {
	        var app = new AspNetApplication<TApplicationType>();
	        configure = configure ?? (a => { });
	        configure(app);
			return BuildClient(app);
        }

		private static System.Net.Http.HttpClient BuildClient(IRequestProcessor app)
		{
			return new System.Net.Http.HttpClient(new PlasmaMessageHandler(app)) {BaseAddress = new Uri("http://localhost")};
        }
    }
}