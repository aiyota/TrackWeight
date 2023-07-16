using System.Net;

namespace TrackWeight.Api.Common.Errors;

public class WeightRecordNotFoundException : Exception, IServiceException
{
    private readonly int _id;

    public WeightRecordNotFoundException(int id) 
    {
        _id = id;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage  => $"Weight record with id {_id} not found";
}
