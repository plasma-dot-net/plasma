using System;
using System.Collections.Generic;

namespace Plasma.Core
{
	public class IntegratedPipeline : IHostPipeline
	{
		public void Configure(AspNetApplication app)
		{
			throw new NotImplementedException();
		}

		public int ProcessRequest(string requestFilePath, string requestPathInfo, string requestQueryString, string requestMethod,
			IEnumerable<KeyValuePair<string, string>> requestHeaders, byte[] requestBody, out List<KeyValuePair<string, string>> responseHeaders, out byte[] responseBody,
			out string responseStatusDescription)
		{
			throw new NotImplementedException();
		}
	}
}