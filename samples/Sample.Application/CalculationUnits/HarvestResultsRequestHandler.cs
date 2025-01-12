using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Graphlib.Algos;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample.Application.ReportModel;

namespace Sample.Application.CalculationUnits;

public class HarvestResultsRequestHandler : IRequestHandler<HarvestResultsRequest>
{
    private readonly ICalculationUnitRepository _calculationUnitRepository;
    private readonly ICalculationResultRepository _calculationResultRepository;
    private readonly ICalculationGraphRepository _calculationGraphRepository;
    private readonly ILogger<HarvestResultsRequestHandler> _logger;

    public HarvestResultsRequestHandler(
        ICalculationResultRepository calculationResultRepository,
        ICalculationUnitRepository calculationUnitRepository,
        ICalculationGraphRepository calculationGraphRepository,
        ILogger<HarvestResultsRequestHandler> logger)
    {
        _calculationResultRepository = calculationResultRepository;
        _calculationUnitRepository = calculationUnitRepository;
        _calculationGraphRepository = calculationGraphRepository;
        _logger = logger;
    }

    public async Task Handle(HarvestResultsRequest request, CancellationToken cancellationToken)
    {
        // Get CalculationUnit context
        var calulationUnitId = request.CalculationUnitId;

        var calulationUnit = await _calculationUnitRepository.GetById(calulationUnitId);

        if (calulationUnit == null)
        {
            _logger.LogError($"Cannot find calculation unit with id={calulationUnitId}");
            // TODO: Save error result
            return;
        }

        // Load graph
        var graph = await _calculationGraphRepository.GetById(calulationUnit.GraphId);

        if (graph == null)
        {
            _logger.LogError($"Cannot find graph with id={calulationUnit.GraphId}");
            // TODO: Save error result
            return;
        }

        var results = new List<ReportDataItem>();

        // Проходим по всем вершинам графа, например, в глубину
        // и собираем результаты вычислений
        foreach (var vertex in graph.DFS())
        {
            var unitResults = await _calculationResultRepository.GetByCalculationUnitId(vertex.Value.Id);

            foreach (var item in unitResults)
            {
                var reportDataItem = item.Content as ReportDataItem;

                if (reportDataItem == null)
                {
                    _logger.LogError($"Cannot parse content for ResultItemId={item.Id} ContentType={item.ContentType}");
                    continue;
                }

                reportDataItem.CalculationUnitIndex = vertex.Index;
                results.Add(reportDataItem);
            }
        }

        // Save report results
        var result = new CalculationResultItem
        {
            Id = Guid.NewGuid(),
            CalculationUnitId = calulationUnitId,
            Content = results.ToArray(),
            Name = "Report Results"
        };
        _logger.LogInformation($"Saving report result for graphId={graph.Id}. DataItemsCount={results.Count}");
        await _calculationResultRepository.Insert(result);
    }
}
