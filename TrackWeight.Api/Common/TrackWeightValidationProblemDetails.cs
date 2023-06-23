using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TrackWeight.Api.Common;

public class TrackWeightValidationProblemDetails : ValidationProblemDetails
{
    private readonly ModelStateDictionary _modelStateDictionary;

    public TrackWeightValidationProblemDetails(ModelStateDictionary modelStateDictionary) 
    {
        _modelStateDictionary = modelStateDictionary;
    }

    public new IEnumerable<string> Errors { get => _modelStateDictionary.Values
                                                    .SelectMany(x => x.Errors)
                                                    .Select(x => x.ErrorMessage); }
}
