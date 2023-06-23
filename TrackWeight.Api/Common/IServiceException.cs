using System.Net;

namespace TrackWeight.Api.Common;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}

