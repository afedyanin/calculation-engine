using CalculationEngine.Core.Model;
using CalculationEngine.Core.Repositories;
using CalculationEngine.Graphlib.Algos;
using MediatR;
using Sample.Application.ReportModel;

namespace Sample.Application.CalculationUnits;

public class HarvestResultsRequestHandler : IRequestHandler<HarvestResultsRequest>
{
    private readonly ICalculationUnitRepository _calculationUnitRepository;
    private readonly ICalculationResultRepository _calculationResultRepository;
    private readonly ICalculationGraphRepository _calculationGraphRepository;

    public HarvestResultsRequestHandler(
        ICalculationResultRepository calculationResultRepository,
        ICalculationUnitRepository calculationUnitRepository,
        ICalculationGraphRepository calculationGraphRepository)
    {
        _calculationResultRepository = calculationResultRepository;
        _calculationUnitRepository = calculationUnitRepository;
        _calculationGraphRepository = calculationGraphRepository;
    }

    public async Task Handle(HarvestResultsRequest request, CancellationToken cancellationToken)
    {
        // Get CalculationUnit context
        var calulationUnitId = request.CalculationUnitId;

        var calulationUnit = await _calculationUnitRepository.GetById(calulationUnitId)
            ?? throw new InvalidOperationException($"Cannot find calculation unit with id={calulationUnitId}");

        // Load graph
        var graph = await _calculationGraphRepository.GetById(calulationUnit.GraphId)
            ?? throw new InvalidOperationException($"Cannot find graph with id={calulationUnit.GraphId}");

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
        };

        await _calculationResultRepository.Insert(result);
    }
}
