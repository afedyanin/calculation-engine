using System.Text.Json;
using CalculationEngine.AppDemo.Stubs;
using CalculationEngine.Core.Helpers;

using MediatR;

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

        var restored = SerializationHelper.Deserialize(json, typeString);

        Assert.That(restored, Is.Not.Null);
        Assert.That(restored.GetType(), Is.EqualTo(Type.GetType(typeString)));
    }
}
