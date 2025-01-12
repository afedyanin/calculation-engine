using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample.Application.ReportModel;

namespace Sample.Application.CalculationUnits;
internal class ColoredCalcRequestHandler : IRequestHandler<ColoredCalcRequest>
{
    private readonly ICalculationUnitRepository _calculationUnitRepository;
    private readonly ICalculationResultRepository _calculationResultRepository;
    private readonly ILogger<ColoredCalcRequestHandler> _logger;

    public ColoredCalcRequestHandler(
        ICalculationResultRepository calculationResultRepository,
        ICalculationUnitRepository calculationUnitRepository,
        ILogger<ColoredCalcRequestHandler> logger)
    {
        _calculationResultRepository = calculationResultRepository;
        _calculationUnitRepository = calculationUnitRepository;
        _logger = logger;
    }

    public async Task Handle(ColoredCalcRequest request, CancellationToken cancellationToken)
    {
        // Get CalculationUnit context
        var calulationUnitId = request.CalculationUnitId;

        var calulationUnit = await _calculationUnitRepository.GetById(calulationUnitId)
            ?? throw new InvalidOperationException($"Cannot find calculation unit with id={calulationUnitId}");

        // It is normal we don't have JobId yet.
        var jobId = calulationUnit.JobId ?? "Unassigned yet.";
        _logger.LogInformation($"Start executing Job={jobId}");

        // Emulate work
        await Task.Delay(request.Delay);

        // Create Report Result
        var reportData = new ReportDataItem
        {
            CreatedDate = DateTime.UtcNow,
            Color = request.Color,
            JobId = jobId,
            Name = $"Some result for {calulationUnit.Id}",
        };

        // Save result
        var result = new CalculationResultItem
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = calulationUnitId,
            Content = reportData,
        };

        await _calculationResultRepository.Insert(result);

        _logger.LogInformation($"End executing Job={jobId}");
    }
}
