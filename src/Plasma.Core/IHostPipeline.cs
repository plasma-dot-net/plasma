using System.Collections.Generic;

namespace Plasma.Core
{
	public interface IHostPipeline
	{
		void Configure(AspNetApplication app);

		int ProcessRequest(
			string requestFilePath,
			string requestPathInfo,
			string requestQueryString,
			string requestMethod,
			IEnumerable<KeyValuePair<string, string>> requestHeaders,
			byte[] requestBody,
			out List<KeyValuePair<string, string>> responseHeaders,
			out byte[] responseBody,
			out string responseStatusDescription);
	}
}