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
        var calculationUnitId = request.CalculationUnitId;
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        var calculationUnit = await GetCalculationUnitWithJobId(calculationUnitId, cts.Token);

        if (calculationUnit == null)
        {
            _logger.LogError($"Cannot find calculation unit with id={calculationUnitId}");
            return;
        }

        var jobId = calculationUnit.JobId!;
        _logger.LogInformation($"Start executing Job={jobId}");

        // Emulate work
        await Task.Delay(request.Delay);

        // Create Report Result
        var reportData = new ReportDataItem
        {
            CreatedDate = DateTime.UtcNow,
            Color = request.Color,
            JobId = jobId,
            Name = $"Some result for {calculationUnitId}",
        };

        // Save result
        var result = new CalculationResultItem
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = calculationUnitId,
            Content = reportData,
        };

        await _calculationResultRepository.Insert(result);

        _logger.LogInformation($"End executing Job={jobId}");
    }

    // Hangfire агент может стартовать выполнение джобы до того, как обновится объект CaclulationUnit в БД
    // в этом случае плде JobId будет не заполненным.
    // Метод ожидает присвоения JobId
    private async Task<CalculationUnit?> GetCalculationUnitWithJobId(Guid calculationUnitId, CancellationToken cancellationToken = default)
    {
        var calulationUnit = await _calculationUnitRepository.GetById(calculationUnitId);

        if (calulationUnit == null)
        {
            // Not found
            return null;
        };

        var jobId = calulationUnit.JobId ?? string.Empty;

        while (string.IsNullOrEmpty(jobId))
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

            calulationUnit = await _calculationUnitRepository.GetById(calculationUnitId);
            jobId = calulationUnit!.JobId ?? string.Empty;

            if (cancellationToken.IsCancellationRequested)
            {
                return calulationUnit;
            }
        }

        return calulationUnit;
    }
}
