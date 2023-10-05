namespace TimeCalculator.Workers;

using TimeCalculator.Calculators;
using TimeCalculator.Extensions;
using TimeCalculator.Models;

/// <summary>
/// Рабочий приложения.
/// </summary>
public class AppWorker
{
    /// <summary>
    /// Работать работу.
    /// </summary>
    public void DoWork()
    {
        var timeCalculationInfo = GetTimeCalculationInfoFromUser();

        var countOfGenerations =
            Console.WriteMessageAndTryGetValue<int>("Введите кол-во вариантов последовательностей:");

        var timeCalculator = new TimeCalculator(timeCalculationInfo);
        var calculatedRanges = timeCalculator.CalculateRanges(countOfGenerations);

        calculatedRanges.PrintTimes();
    }

    /// <summary>
    /// Получить информацию для расчетов времени от пользователя.
    /// </summary>
    /// <returns>Информация для расчетов времени.</returns>
    private TimeCalculationInfo GetTimeCalculationInfoFromUser()
    {
        var neededHours = Console.WriteMessageAndTryGetValue<int>("Введите необходимые часы для занятия делом:");
        var neededDays =
            Console.WriteMessageAndTryGetValue<int>("Введите кол-во дней, которое вы хотите заниматься делом:");
        var availableHoursInDay =
            Console.WriteMessageAndTryGetValue<int>("Введите максимальное кол-во часов для занятия делом в день:");

        var timeCalculationInfo = new TimeCalculationInfo(neededHours, neededDays, availableHoursInDay);

        return timeCalculationInfo;
    }
}