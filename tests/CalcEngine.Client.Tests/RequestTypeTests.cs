using System.Reflection;
using CalcEngine.Application.Handlers.SimpleReport;
using CalcEngine.Client.Extensions;
using Hangfire.Common;
using Hangfire.Storage;
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

    [Test]
    public void CanGetMethodInfo()
    {
        var type = typeof(Mediator);
        var methodInfo = type.GetMethods().FirstOrDefault(
            x => x.Name.Equals(nameof(Mediator.Send), StringComparison.OrdinalIgnoreCase)
            && x.IsGenericMethod
            && x.GetParameters().Length == 2);

        Assert.That(methodInfo, Is.Not.Null);
        var genMethod = methodInfo.MakeGenericMethod(typeof(PrepareSimpleReportRequest));

        var paramTypes = genMethod.GetParameters();
        var sendParams = string.Join(",", paramTypes.Select(p => p.ParameterType.Name));
        Console.WriteLine($"Send params: {sendParams}");
    }

    [Test]
    public void CanSerializeJob()
    {
        var guid = Guid.NewGuid();
        var request = new PrepareSimpleReportRequest()
        {
            CorrelationId = guid,
        };

        var job = BackgroundJobExtensions.CreateJob(request);
        var invocationData = InvocationData.SerializeJob(job);
        var payload = invocationData.SerializePayload();

        Assert.That(job, Is.Not.Null);

        Console.WriteLine($"payload={payload}");
    }

    [Test]
    public void CanGetInvocationData()
    {
        var guid = Guid.NewGuid();
        var request = new PrepareSimpleReportRequest()
        {
            CorrelationId = guid,
        };

        var job = BackgroundJobExtensions.CreateJob(request);

        var invocationData = InvocationData.SerializeJob(job);

        var payload = invocationData.SerializePayload();
        Console.WriteLine($"{payload}");
    }

    [Test]
    public void CanGetInvocationDataFromExpression()
    {
        var guid = Guid.NewGuid();
        var request = new PrepareSimpleReportRequest()
        {
            CorrelationId = guid,
        };

        var job = BackgroundJobExtensions.CreateJob(request);

        var invocationData = InvocationData.SerializeJob(job);

        var payload = invocationData.SerializePayload();
        Console.WriteLine($"{payload}");
    }


    [Test]
    public void CanCreateJob()
    {
        var guid = Guid.NewGuid();
        var request = new PrepareSimpleReportRequest()
        {
            CorrelationId = guid,
        };

        var job = BackgroundJobExtensions.CreateJob(request);
        Assert.That(job, Is.Not.Null);
    }

    [Test]
    public void CanDeserializeRequest()
    {
        var json = /*lang=json,strict*/ "{\"CorrelationId\":\"ef688b84-ae3c-4e4f-95a6-7bd4f1b64c10\"}";
        var correlationId = new Guid("ef688b84-ae3c-4e4f-95a6-7bd4f1b64c10");
        var request = JsonConvert.DeserializeObject<PrepareSimpleReportRequest>(json);
        Assert.That(request, Is.Not.Null);
        Assert.That(request.CorrelationId, Is.EqualTo(correlationId));
    }

    public static Job CreateJobFromExpression<T>(T request) where T : IRequest
    {
        var job = Job.FromExpression<IMediator>(m => m.Send(request, CancellationToken.None));
        return job;
    }

}
