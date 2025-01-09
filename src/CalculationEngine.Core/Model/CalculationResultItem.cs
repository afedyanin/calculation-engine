using CalculationEngine.Core.Helpers;

namespace CalculationEngine.Core.Model;

public class CalculationResultItem
{
    private string? _contentType;
    private string? _contentJson;
    private object? _content;

    public Guid Id { get; set; }

    public Guid CalculationUnitId { get; set; }

    public CalculationUnit CalculationUnit { get; set; } = null!;

    public string? Name { get; set; }

    public object? Content
    {
        get
        {
            if (_content != null)
            {
                return _content;
            }

            if (_contentJson == null || _contentType == null)
            {
                return null;
            }

            _content = SerializationHelper.Deserialize(_contentJson, _contentType);
            return _content;
        }
        set
        {
            if (value == null)
            {
                _content = null;
                _contentType = null;
                _contentJson = null;
            }
            else
            {
                (_contentJson, _contentType) = SerializationHelper.Serialize(value);
                _content = value;
            }
        }
    }

    public string? ContentType => _contentType;

    public string? ContentJson => _contentJson;
}
