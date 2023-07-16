using System.Net;
using TrackWeight.Api.Common;

public class CalorieRecordNotFoundException : Exception, IServiceException
{
    private readonly int _id;

    public CalorieRecordNotFoundException(int id)
    {
        _id = id;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage => $"Calorie record with id {_id} not found";
}