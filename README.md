# hf-demo

HangFire ASP.NET Host with npgsql

## Resources

- [Hangfire](https://github.com/HangfireIO/Hangfire)
- [Docs](https://docs.hangfire.io/en/latest/index.html)
- [Hangfire.PostgreSql](https://github.com/hangfire-postgres/Hangfire.PostgreSql)
- [Обзор библиотек для Hangfire](https://habr.com/ru/articles/764690/)
- https://stackoverflow.com/questions/59196989/get-all-succeeded-jobs-with-hangfire-monitoring-api


- [Dataflow-TPL-RU](https://learn.microsoft.com/ru-ru/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [DataFlow-TPL-EN](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [Проверка графа на наличие циклов](https://brestprog.by/topics/graphcycles)

## Docs

Расчетная задача формируется в виде графа вычислений, представляющего собой направленный ациклический граф - DAG.
Каждый узел графа - расчетная единица (Calculation Unit).
Расчетные единицы одного графа вычислений могут выполняться на различных процессах (машинах) расчетного кластера.

### Техническая реализация 

Технически, Calculation Unit - это задача (Job), выполняемая HangFire агентом. 
С точки зрения кода Calculation Unit представляет собой Mediator Handler. 
Handler реализуюет обработку реквеста, заданного наследником интерфейса IRequest.
Каждый handler внутри себя может реализовать свой собственный граф вычислений.

### Клиентская часть

Клиент формирует задачу через создание графа вычислений с помощью методов CalculationBuilder:

- source
- sequential
- fork
- join

Граф вычислений валидируется на отсутствие циклов и сохраняется в БД (?)
Если узел графа предусматривает синхронизацию задач (join) - происходит ожидание завершения выполнения предварительных задач.

Процесс вычислений реализуется через
- парсинг узлов графа вычислений и постановка задач (Job) в HangFire
- мониторинг выполнения всех задач графа и обновление состояния

## Передача параметров и доступ к контексту вычислений

Параметры в джобах передаются только на вход (OneWay)
Для того чтобы обеспечить коммуникацию и обмен данными между отдельными единицами вычислений используется 
контекст вычислений ICalculationContext, который представляет собой репозиторий для хранения:
- метаданных графа вычислений и его состояния
- метаданных всех узлов графа вычислений и их состояний
- результатов вычислений конкретного узла - NodePayload

Для взаимодействия с контекстом вычислений (например, для сохранениия данных расчета или получения данных из предыщущих узлов)
необходимо:
- обеспечить передачу ID текущего узла в хендлер. Для этого используется интерфейс ICalculationRequest - наследник от IRequest
- в хендлере заинжектить интерфейс ICalculationContext
- воспользоваться методами ICalculationContext
- - для получения графа вычислений
  - для получения текущего узла вычислений
  - для загрузки\сохранения данных вычислений


 








