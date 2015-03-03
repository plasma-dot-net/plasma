using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Web;

namespace Plasma.Core
{
	public class IntegratedPipeline : IHostPipeline
	{
		public void Configure(AspNetApplication app)
		{
			typeof(HttpRuntime).GetField("_useIntegratedPipeline",	BindingFlags.Static | BindingFlags.NonPublic).SetValue(typeof(HttpRuntime), true);
			typeof(HttpRuntime).GetField("_iisVersion",				BindingFlags.Static | BindingFlags.NonPublic).SetValue(typeof(HttpRuntime), new Version(7, 0));

			var moduleConfigurationInfoType = typeof(HttpRuntime).Assembly.GetType("System.Web.ModuleConfigurationInfo", true);
			var instanceOfModuleConfigInfo = Activator.CreateInstance(typeof(List<>).MakeGenericType(moduleConfigurationInfoType));
			typeof(HttpApplication).GetField("_moduleConfigInfo",	BindingFlags.Static | BindingFlags.NonPublic).SetValue(typeof(HttpRuntime), instanceOfModuleConfigInfo);
		}

		public int ProcessRequest(string requestFilePath, string requestPathInfo, string requestQueryString, string requestMethod,
			IEnumerable<KeyValuePair<string, string>> requestHeaders, byte[] requestBody, out List<KeyValuePair<string, string>> responseHeaders, out byte[] responseBody,
			out string responseStatusDescription)
		{
			var wr = new WorkerRequest(requestFilePath, requestPathInfo,
				requestQueryString, requestMethod, requestHeaders, requestBody);

			typeof (HttpRuntime).GetMethod("ProcessRequestNow", BindingFlags.NonPublic | BindingFlags.Static)
								.Invoke(typeof (HttpRuntime), new object[] {wr});

			while (!wr.Completed)
			{
				Thread.Sleep(50);
			}

			responseHeaders = wr.ResponseHeaders;
			responseBody = wr.ResponseBody;
			responseStatusDescription = wr.ResponseStatusDescription;
			return wr.ResponseStatus;
		}
	}
}