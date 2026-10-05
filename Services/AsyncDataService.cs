using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WtLab2.Models;

namespace WtLab2.Services;

public class AsyncDataService
{
    public async Task SimulateProcessingAsync(string operationName, int delayMs = 1000)
    {
        Console.WriteLine($"[async] Начало: {operationName}");
        await Task.Delay(delayMs);
        Console.WriteLine($"[async] Завершено: {operationName}");
    }

    public async Task<List<Project>> LoadProjectsAsync(CancellationToken cancellationToken = default)
    {
        var result = new List<Project>();

        try
        {
            Console.WriteLine("[async] Загрузка проектов из внешнего API...");
            await Task.Delay(1500, cancellationToken);

            result.Add(new Project("CRM System", "ООО Ромашка", 1500000m, DateTime.Now.AddMonths(6), "InProgress"));
            result.Add(new Project("Mobile Banking", "АО Банк", 3200000m, DateTime.Now.AddMonths(9), "New"));
            result.Add(new Project("HR Portal", "ИП Иванов", 450000m, DateTime.Now.AddMonths(3), "Done"));

            Console.WriteLine($"[async] Загружено проектов: {result.Count}");
            return result;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[async] Загрузка проектов отменена.");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[async] Ошибка загрузки: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("[async] finally: освобождение ресурсов загрузки проектов.");
        }
    }

    public async Task<List<ProjectTask>> LoadTasksAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        try
        {
            Console.WriteLine("[async] Загрузка задач проекта...");
            await Task.Delay(1200, cancellationToken);

            return new List<ProjectTask>
            {
                new ProjectTask("Разработка API", "REST API для проекта", projectId, "High", 40, "InProgress"),
                new ProjectTask("Дизайн интерфейса", "Макеты Figma", projectId, "Medium", 20, "New"),
                new ProjectTask("Тестирование", "Интеграционные тесты", projectId, "High", 30, "New")
            };
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[async] Загрузка задач отменена.");
            throw;
        }
        finally
        {
            Console.WriteLine("[async] finally: завершение загрузки задач.");
        }
    }
}