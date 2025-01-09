using CalculationEngine.Core.Handlers;
using CalculationEngine.Core.Helpers;

namespace CalculationEngine.Core.Model;

public class CalculationUnit
{
    private string? _requestType;
    private string? _requestJson;
    private ICalculationRequest? _request;

    public Guid Id { get; set; }

    public Guid GraphId { get; set; }

    public string? JobId { get; set; }

    public required ICalculationRequest Request
    {
        get
        {
            if (_request != null)
            {
                return _request;
            }

            if (_requestJson == null || _requestType == null)
            {
                throw new InvalidOperationException("Cannot initialize request type.");
            }

            _request = SerializationHelper.Deserialize(_requestJson, _requestType) as ICalculationRequest;

            if (_request == null)
            {
                throw new InvalidCastException("Cannot cast request object to ICalculationRequest type.");
            }

            return _request;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            (_requestJson, _requestType) = SerializationHelper.Serialize(value);
            _request = value;
        }
    }

    public ICollection<CalculationResultItem> Results { get; } = [];
}
