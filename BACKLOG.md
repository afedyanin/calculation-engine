# 2025-01-20

## dotnet pack include ref projects

- https://dev.to/yerac/include-both-nuget-package-references-and-project-reference-dll-using-dotnet-pack-2d8p
- https://josef.codes/dotnet-pack-include-referenced-projects/

```pshell
dotnet pack -c Release -p:NuspecFile=Vertr.CalculationEngine.nuspec

```





# 2025-01-11

## Moq
https://habr.com/ru/articles/150859/
https://github.com/devlooped/moq
https://metanit.com/sharp/mvc5/18.5.php


# 2025-01-10

## TODO

- реквест и хендлер загрузки и парсинга графа
- закрыть конструкторы? валидация моделей - https://learn.microsoft.com/en-us/ef/core/modeling/constructors
- использовать upsert в DAL? (выставление ID на уровне репозитория - использовать Guid.Empty как флаг новой сущности) - https://stackoverflow.com/questions/16195847/does-ef-upsert-have-to-be-done-manually
- разделить сервер на API и HF агента
- обработка графа - вспомогательные методы
    - поиск мест и вставка ожидающих узлов
    - источники
    - стоки
- постановка задач из графа в HF
- контрольный пример генерации репорта - кейс расписать на бумажке и в md
- прогресс и статус задач?
- проверить еще раз сериализацию реквеста со сложными полями - IDictionary, ссылки на объекты
- подготовить базовое описание

# 2025-01-09

## TODO DAL
- (!) сохранение графа и его вершин в БД
- (!) восстановление графа и его вершин из БД
- написать тесты на DAL 
- репозиторий к объектам DAL

## Cases

- - Создать граф на основе реквестов 
- Сохранить граф в БД на основе реквестов
- Создать реквест и поставить джобу на загрузку и парсинг графа из HF агента

## EF Core

- [Getting Started with EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
- [NpgSql](https://www.npgsql.org/efcore/)
- [Введение в Entity Framework Core](https://metanit.com/sharp/efcore/1.1.php)

### Migrations

```shell
dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate

dotnet ef database update
```



# 2025-01-07

## Links
- [Create Graph Online](https://graphonline.top/en/)

## Backlog

- сохранение (сериализация-десериализация) объектов Graph, Node, NodePayload в БД
- сервис по работе с контекстом: 
-- получить граф и его состояние (статус завершения задач например)
-- получить произвольную ноду и ее метаданные (статус завершения джобы, например)
-- получить пейлоад для ноды
-- сохранить пейлоад для ноды
- разобраться с ручной установкой HF базы и выключением опции автоматического ее создания
- выбор фреймворка для реализации DAL: EFCore vs Dapper
 
# 2025-01-05

## Бэклог

### JonEnqueueHelper (Extensions)
- 
- пофискисть сериализацию коллекций IRequest
- реализовать рекаринг задачу для Join с отменой рекаринга
- отделить структуру данных и ее построение от контекста медиатора и хф
- вначале построить структуру и лишь затем ее использовать для генерации юнитов, задач и т.п.
- проверить работу тригера при окончании работы рекаринг джобы


## Graph Data Structure
- https://github.com/YaccConstructor/QuickGraph/tree/master
- https://www.simplilearn.com/tutorials/c-sharp-tutorial/what-is-graphs-in-c-sharp
- https://masterdotnet.com/csharp/ds/graphincsharp/
- https://debug.to/3320/how-to-use-graph-and-bfs-in-c

## Tree Data Structure 
- https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
- https://www.c-sharpcorner.com/article/tree-data-structure/
- https://www.geeksforgeeks.org/introduction-to-tree-data-structure/
- https://www.designgurus.io/answers/detail/how-to-implement-a-tree-data-structure-in-c
- https://debug.to/3253/tree-in-data-structure-using-c
- https://stackoverflow.com/questions/9860207/build-a-simple-high-performance-tree-data-structure-in-c-sharp/17995531

# 2025-01-04

## Базовые тесты
- создать отдельный узел и выполнить
- создать цепочку узлов и выполнить
- создать fork и выполнить все узлы

## Recurring vs await
-  join через recurring job

## Mониторинг
- состояние и завершение отдельного узла
- состояние и завершение всего графа
- обработка ошибок и таймаутов


 
# 2025-01-02

## Бэклог задач

### API обвязка вокруг репорта на стороне контроллера
- команда на генерацию репорта
- проверка статуса и прогресса
- получение данных репорта
- отмена построения репорта

Все команды через медиатор, но они общие для всех репортов. Где разместить хендлеры для них?

### Клиентский пакет
- нужно выделить базовые классы и клиента для постановки джобы в отдельную сборку и нугет пакет

### Background Service по мониторингу джобов
- создать сервис
- мониторить джобы и складывать их состояние в общедоступный словарь

### Awating Service
- базовая версия
- фикс багов и зависаний
- канцел TCS
- интеграция с севисом мониторинга джобов

### Пример Fork-Join 
- через стандартные механизмы await
- канцел задачи
- логирование



# 2024-12-28

## Ключевые решения
- для сплитинга джобов на стороне репортинга исопльзовать ContinueJobWith. Создание репорта будет состоять из двух джобов - Calculation и Build, соединенных через ContinueJobWith на стороне репортинг сервиса
- на стороне HF агента fork-join паттерн для дочерних задач реализуется через awaiting service + monitoring service. Последний финиширует TCS при завершении джоба.

## Бэклог задач
- реализовать два клиентских хендлера для построения отчета - Calc и Build
- проверить согласованную работу джобов в ContinueWith - репорт создается по результатам расчетов
- проверить работу при решедулинге основной джобы, рестарте сервера, файла и/или канцела одного из джобов - составить тестплан
- реализовать базовые классы/обертки/экстеншены для хэндлеров и реквестов
- разобраться с экспирацией джобов - как можно использовать?
- продумать систему организации классов хендлеров и реквестов в ApplicationServer
- реализовать awating service. Проверить корректную работу cancel-а. Устранить зависания TCS через таймауты и/или экспирацию.
- реализовать jobmonitoring service
- реализовать простой fork-join для расчетной задачи
- протестировать работу fork-join в краевых кейсах - рестарты, эксепшены, канцелы - составить тестплан
- продумать доменную модель и DAL для расчетных задач
- реализовать DAL через EFCore: хранение стейта, параметров, результатов расчета, прогресса - (!) не дублировать логику HF
- реализовать прогресс по задаче стандартными средствами IProgress
- продумать систему логирования и контролей работоспособности
- продумать метрики и интеграцию с OpenTelemetry
- написать документацию в формате md
- продумать архитектуру пакетов и деплоя
- продумать версионирование и масштабирование
- реализовать расчетный кластер из трех нод. Протестировать сборку репорта через fork-join расчеты. Проверить краевые кейсы


# 2024-12-27

- Admin API for Job Management
- EFCore - DB Code First

# 2024-12-26

- (!) репозиторий и БД для хранениия стейта расчета и результатов расчета
- (!) доменная модель для расчетов - избавиться от report
- передача параметров
- контроль прогресса выполнения задачи
- статусная модель задачи
- использование врапперов
- cancelation, errors, logging
- audit
- open telemetry
- fork-join pattern



- [Hangfire](https://github.com/HangfireIO/Hangfire)
- [Docs](https://docs.hangfire.io/en/latest/index.html)
- [Hangfire.PostgreSql](https://github.com/hangfire-postgres/Hangfire.PostgreSql)
- [Обзор библиотек для Hangfire](https://habr.com/ru/articles/764690/)
- https://stackoverflow.com/questions/59196989/get-all-succeeded-jobs-with-hangfire-monitoring-api


- [Dataflow-TPL-RU](https://learn.microsoft.com/ru-ru/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [DataFlow-TPL-EN](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [Проверка графа на наличие циклов](https://brestprog.by/topics/graphcycles)



