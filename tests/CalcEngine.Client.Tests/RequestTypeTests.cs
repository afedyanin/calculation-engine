using CalcEngine.Application.Handlers.SimpleReport;
using MediatR;
using Newtonsoft.Json;

namespace CalcEngine.Client.Tests;

[TestFixture(Category = "Unit")]
public class RequestTypeTests
{
    [Test]
    public void CanRestoreRequestType()
    {
        var guid = Guid.NewGuid();
        var requests = new List<IRequest>
        {
            new PrepareSimpleReportRequest()
            {
                CorrelationId = guid,
            },
            new BuildSimpleReportRequest()
            {
                CorrelationId = guid,
            }
        };

        foreach (var request in requests)
        {
            var json = JsonConvert.SerializeObject(request);
            Console.WriteLine($"type={request.GetType().Name} json={json}");
        }

        Assert.Pass();
    }
}
