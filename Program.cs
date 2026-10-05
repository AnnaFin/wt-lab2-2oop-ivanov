using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WtLab2.Interfaces;
using WtLab2.Models;
using WtLab2.Services;

namespace WtLab2;

public class Program
{
    public static async System.Threading.Tasks.Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PrintHeader();
        await ShowMenuAsync();
    }

    private static void PrintHeader()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("Лабораторная работа №2");
        Console.WriteLine("Вариант 11: IT-компания / Управление проектами и задачами");
        Console.WriteLine("============================================================");
    }

    private static async System.Threading.Tasks.Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("\nГлавное меню:");
            Console.WriteLine("1 - Блок 1");
            Console.WriteLine("2 - Блок 2");
            Console.WriteLine("3 - Блок 3");
            Console.WriteLine("0 - Выход");
            Console.Write("Введите: ");

            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    RunBlock1();
                    break;
                case "2":
                    RunBlock2();
                    break;
                case "3":
                    await RunBlock3Async();
                    break;
                case "0":
                    Console.WriteLine("Завершение работы.");
                    return;
                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }
    }

    private static void RunBlock1()
    {
        Console.WriteLine("БЛОК 1. УРОВЕНЬ 1");

        var project1 = new Project("CRM System", "ООО Ромашка", 1500000m, DateTime.Now.AddMonths(6), "InProgress");

        // object initializer
        var project2 = new Project("Mobile Banking", "АО Банк", 3200000m, DateTime.Now.AddMonths(9))
        {
            Customer = "АО Банк (VIP-клиент)",
            Deadline = DateTime.Now.AddMonths(10)
        };

        var developer1 = new Developer("Анна Финдрик", "annafindrik@gmail.com", "Senior", 250000m);
        var task1 = new ProjectTask("Разработка API", "REST API для CRM", project1.Id, "High", 40, "InProgress");
        task1.AssignDeveloper(developer1.Id);

        project1.PrintInfo();
        project2.PrintInfo();
        developer1.PrintInfo();
        task1.PrintInfo();

        Console.WriteLine("\nБЛОК 1. УРОВЕНЬ 2");

        project1.UpdateBudget(1700000m);
        Console.WriteLine($"Новый бюджет проекта 1: {project1.Budget:C}");

        try
        {
            project1.UpdateBudget(-100m);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка валидации бюджета: {ex.Message}");
        }

        BaseEntity[] entities = { project1, project2, developer1, task1 };

        foreach (var entity in entities)
        {
            entity.PrintInfo(); // полиморфный вызов
            Console.WriteLine($"StorageInfo: {entity.GetStorageInfo()}");
        }

        Console.WriteLine("\nБЛОК 1. УРОВЕНЬ 3");

        IEntityService<Project> projectService = new EntityService<Project>();

        projectService.Add(project1);

        // перегрузка Add(IEnumerable<T>)
        projectService.Add(new[]
        {
            project2,
            new Project("HR Portal", "ИП Финдрик", 450000m, DateTime.Now.AddMonths(3), "New")
        });

        Console.WriteLine($"Количество проектов в сервисе: {projectService.Count}");

        var foundProject = projectService.GetById(project1.Id);
        if (foundProject != null)
        {
            Console.WriteLine("Найден проект по Id:");
            foundProject.PrintInfo();
        }

        var allProjects = projectService.GetAll();
        Console.WriteLine("Все проекты через интерфейсную ссылку:");
        foreach (var project in allProjects)
        {
            Console.WriteLine($"- {project.Name}, бюджет {project.Budget:C}");
        }

        Console.WriteLine("protected-члены используются в BaseEntity.SetEntityType() и доступны только наследникам.");
    }

    private static void RunBlock2()
    {
        Console.WriteLine("БЛОК 2. УРОВЕНЬ 1");

        var projectA = new Project("CRM System", "ООО Ромашка", 1500000m, DateTime.Now.AddMonths(6), "InProgress");
        var projectB = new Project("Mobile Banking", "АО Банк", 3200000m, DateTime.Now.AddMonths(9), "New");
        var projectC = new Project("HR Portal", "ИП Иванов", 450000m, DateTime.Now.AddMonths(3), "Done");

        var tasks = new List<ProjectTask>
        {
            new ProjectTask("Разработка API", "REST API", projectA.Id, "High", 40, "InProgress"),
            new ProjectTask("Дизайн интерфейса", "Макеты Figma", projectA.Id, "Medium", 20, "New"),
            new ProjectTask("Тестирование", "Интеграционные тесты", projectA.Id, "High", 30, "New"),
            new ProjectTask("Мобильный экран", "Экран переводов", projectB.Id, "High", 35, "InProgress"),
            new ProjectTask("Интеграция с банком", "API банка", projectB.Id, "High", 50, "New"),
            new ProjectTask("Кадровый отчет", "Отчёт по сотрудникам", projectC.Id, "Low", 15, "Done"),
            new ProjectTask("Экспорт данных", "Экспорт в Excel", projectC.Id, "Medium", 25, "Done")
        };

        foreach (var task in tasks)
        {
            task.PrintInfo();
        }

        Console.WriteLine("\nБЛОК 2. УРОВЕНЬ 2");

        var developers = new Dictionary<Guid, Developer>
        {
            [Guid.NewGuid()] = new Developer("Анна Финдрик", "annafindrik@gmail.com", "Senior", 250000m),
            [Guid.NewGuid()] = new Developer("Дарья Талько", "dariatalko@gmail.com", "Middle", 180000m),
            [Guid.NewGuid()] = new Developer("Анастасия Правоторова", "pravotorik@gmail.com", "Junior", 90000m)
        };

        var firstDeveloperId = developers.Keys.First();

        if (developers.TryGetValue(firstDeveloperId, out var developer))
        {
            Console.WriteLine("Найден разработчик:");
            developer.PrintInfo();
        }

        var missingId = Guid.NewGuid();
        if (!developers.TryGetValue(missingId, out var missingDeveloper))
        {
            Console.WriteLine($"Разработчик с Id={missingId} не найден. TryGetValue вернул false.");
        }

        Console.WriteLine("\nБЛОК 2. УРОВЕНЬ 3");

        var highPriority = tasks
            .Where(t => t.Priority == "High")
            .OrderByDescending(t => t.EstimatedHours)
            .Select(t => new { t.Title, t.Priority, t.EstimatedHours, t.Status })
            .ToList();

        Console.WriteLine("Высоко приоритетные задачи по часам:");
        foreach (var item in highPriority)
        {
            Console.WriteLine($"{item.Title} | {item.Priority} | {item.EstimatedHours} ч | {item.Status}");
        }

        var totalCount = tasks.Count;
        var totalHours = tasks.Sum(t => t.EstimatedHours);
        var avgHours = tasks.Average(t => t.EstimatedHours);
        var minHours = tasks.Min(t => t.EstimatedHours);
        var maxHours = tasks.Max(t => t.EstimatedHours);

        Console.WriteLine($"\nАгрегаты: Количество={totalCount}, Сумма={totalHours}, Среднее={avgHours:F1}, Мин={minHours}, Макс={maxHours}");

        var groupedByStatus = tasks
            .GroupBy(t => t.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count(),
                TotalHours = g.Sum(t => t.EstimatedHours),
                AverageHours = g.Average(t => t.EstimatedHours)
            })
            .OrderBy(g => g.Status);

        Console.WriteLine("\nГруппировка задач по статусу:");
        foreach (var group in groupedByStatus)
        {
            Console.WriteLine($"{group.Status}: количество={group.Count}, всего часов={group.TotalHours}, в среднем часов={group.AverageHours:F1}");
        }

        var groupedByPriority = tasks
            .GroupBy(t => t.Priority)
            .Select(g => new
            {
                Priority = g.Key,
                Count = g.Count(),
                TotalHours = g.Sum(t => t.EstimatedHours)
            });

        Console.WriteLine("\nГруппировка задач по приоритету:");
        foreach (var group in groupedByPriority)
        {
            Console.WriteLine($"{group.Priority}: количество={group.Count}, всего часов={group.TotalHours}");
        }
    }

    private static async System.Threading.Tasks.Task RunBlock3Async()
    {
        var asyncService = new AsyncDataService();

        Console.WriteLine("БЛОК 3. УРОВЕНЬ 1");
        await asyncService.SimulateProcessingAsync("Ну очень длительная операция!", 1000);

        Console.WriteLine("\nБЛОК 3. УРОВЕНЬ 2");
        var projects = await asyncService.LoadProjectsAsync();

        foreach (var project in projects)
        {
            project.PrintInfo();
        }

        Console.WriteLine("\nБЛОК 3. УРОВЕНЬ 3");

        var projectId = projects.First().Id;

        var parallel1 = asyncService.LoadProjectsAsync();
        var parallel2 = asyncService.LoadTasksAsync(projectId);
        var parallel3 = asyncService.SimulateProcessingAsync("Параллельная задача 3", 800);

        await System.Threading.Tasks.Task.WhenAll(parallel1, parallel2, parallel3);
        Console.WriteLine("Все параллельные задачи завершены.");

        Console.WriteLine("\nТокен отмены CancellationToken:");

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(500);

        try
        {
            await asyncService.LoadProjectsAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Операция была отменена через CancellationTokenSource.");
        }
    }
}