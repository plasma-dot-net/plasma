using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace Plasma.Core
{
	public class ClassicPipeline : IHostPipeline
	{
		public void Configure(AspNetApplication app)
		{
		}

		public int ProcessRequest(string requestFilePath, string requestPathInfo, string requestQueryString, string requestMethod,
			IEnumerable<KeyValuePair<string, string>> requestHeaders, byte[] requestBody, out List<KeyValuePair<string, string>> responseHeaders, out byte[] responseBody,
			out string responseStatusDescription)
		{
			var wr = new WorkerRequest(requestFilePath, requestPathInfo,
				requestQueryString, requestMethod, requestHeaders, requestBody);

			HttpRuntime.ProcessRequest(wr);

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