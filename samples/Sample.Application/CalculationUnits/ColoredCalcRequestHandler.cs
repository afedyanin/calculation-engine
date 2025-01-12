using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using MediatR;
using Sample.Application.ReportModel;

namespace Sample.Application.CalculationUnits;
internal class ColoredCalcRequestHandler : IRequestHandler<ColoredCalcRequest>
{
    private readonly ICalculationUnitRepository _calculationUnitRepository;
    private readonly ICalculationResultRepository _calculationResultRepository;

    public ColoredCalcRequestHandler(
        ICalculationResultRepository calculationResultRepository,
        ICalculationUnitRepository calculationUnitRepository)
    {
        _calculationResultRepository = calculationResultRepository;
        _calculationUnitRepository = calculationUnitRepository;
    }

    public async Task Handle(ColoredCalcRequest request, CancellationToken cancellationToken)
    {
        // Emulate work
        await Task.Delay(request.Delay);

        // Get CalculationUnit context
        var calulationUnitId = request.CalculationUnitId;

        var calulationUnit = await _calculationUnitRepository.GetById(calulationUnitId)
            ?? throw new InvalidOperationException($"Cannot find calculation unit with id={calulationUnitId}");

        // Create Report Resul

        var reportData = new ReportDataItem
        {
            CreatedDate = DateTime.UtcNow,
            Color = request.Color,
            JobId = calulationUnit.JobId ?? "None",
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
    }
}
