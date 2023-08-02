namespace TimeCalculator;

using System.Diagnostics;

/// <summary>
/// Конфигуратор приложения.
/// </summary>
public static class AppConfigurator
{
    /// <summary>
    /// Добавить обработчик не обработанных исключений.
    /// </summary>
    public static void AddUnhandledExceptionHandler()
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
    }

    /// <summary>
    /// Обработчик не обработанных исключений
    /// </summary>
    /// <param name="sender">Отправитель исключения.</param>
    /// <param name="e">Аргументы события.</param>
    private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        Console.WriteLine($"Получена не обработанная ошибка: {e.ExceptionObject}");
        Console.WriteLine("Нажмите любую клавишу для перезапуска приложения...");
        Console.ReadKey();

        if (Environment.ProcessPath != null)
        {
            Process.Start(Environment.ProcessPath);
        }

        Environment.Exit(0);
    }
}