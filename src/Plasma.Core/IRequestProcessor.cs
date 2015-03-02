namespace Plasma.Core
{
    public interface IRequestProcessor
    {
		bool UseIntegratedPipeline { get; set; }
        AspNetResponse ProcessRequest(AspNetRequest request);
        AspNetResponse ProcessRequest(string requestPath);
    }
}