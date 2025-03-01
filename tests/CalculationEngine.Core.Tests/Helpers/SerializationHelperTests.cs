using System.Text.Json;
using CalculationEngine.Core.Helpers;
using CalculationEngine.Core.Tests.Stubs;
using MediatR;
using Sample.Application.ReportModel;

namespace CalculationEngine.Core.Tests.Helpers;

[TestFixture(Category = "Unit")]
public class SerializationHelperTests
{
    [Test]
    public void CanSerializeRequest()
    {
        IRequest request = new DelayRequest
        {
            CalculationUnitId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(123),
        };

        var typeString = $"{request.GetType().FullName}, {request.GetType().Assembly.GetName().Name}";
        Console.WriteLine(typeString);

        var type = Type.GetType(typeString)!;
        Assert.That(type, Is.Not.Null);

        var json = JsonSerializer.Serialize(request, type);
        Console.WriteLine(json);

        var restored = JsonSerializer.Deserialize(json, type);
        Assert.That(restored, Is.Not.Null);
    }


    [Test]
    public void CanSerializeRequestWithHelper()
    {
        IRequest request = new DelayRequest
        {
            CalculationUnitId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(123),
        };

        (var json, var typeString) = SerializationHelper.Serialize(request);

        Assert.Multiple(() =>
        {
            Assert.That(json, Is.Not.Null);
            Assert.That(typeString, Is.Not.Null);
        });

        Console.WriteLine(json);
        Console.WriteLine(typeString);
    }

    [Test]
    public void CanDeserializeRequestWithHelper()
    {
        IRequest request = new DelayRequest
        {
            CalculationUnitId = Guid.NewGuid(),
            Delay = TimeSpan.FromSeconds(123),
        };

        (var json, var typeString) = SerializationHelper.Serialize(request);

        var type = Type.GetType(typeString);
        var restored = SerializationHelper.Deserialize(json, typeString);

        Assert.That(restored, Is.Not.Null);
        Assert.That(restored.GetType(), Is.EqualTo(type));

        var typed = restored as DelayRequest;
        Console.WriteLine($"CalculationUnitId={typed.CalculationUnitId} Delay={typed.Delay}");
    }

    [Test]
    public void CanDeserializeReportDataItem()
    {
        var typeString = "Sample.Application.ReportModel.ReportDataItem, Sample.Application";

        var json = @"
{
  ""Name"": ""Some result for 82ffa159-88e8-4dc6-92d0-8313c3030bf5"",
  ""Color"": ""Green"",
  ""JobId"": ""135"",
  ""Amount"": 0,
  ""CreatedDate"": ""2025-01-12T08:56:17.0866255Z"",
  ""CalculationUnitIndex"": 0
}
";


        var type = Type.GetType(typeString);
        var restored = SerializationHelper.Deserialize(json, typeString);

        Assert.That(restored, Is.Not.Null);
        Assert.That(restored.GetType(), Is.EqualTo(type));

        var typed = restored as ReportDataItem;

        Console.WriteLine($"Color={typed!.Color}");
    }
}
