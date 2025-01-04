# hf-demo

HangFire ASP.NET Host with npgsql

## Resources

- [Hangfire](https://github.com/HangfireIO/Hangfire)
- [Docs](https://docs.hangfire.io/en/latest/index.html)
- [Hangfire.PostgreSql](https://github.com/hangfire-postgres/Hangfire.PostgreSql)
- [Обзор библиотек для Hangfire](https://habr.com/ru/articles/764690/)

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
Подсистема исполнения парсит узлы графа и формирует задачи на выполнение для HangFire.
Если узел графа предусматривает синхронизацию задач (join) - происходит ожидание завершения выполнения предварительных задач.










