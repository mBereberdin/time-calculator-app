namespace TimeCalculator.Workers;

using TimeCalculator.Calculators;
using TimeCalculator.Helpers;

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
        var neededHours = CliHelper.WriteMessageAndTryGetValue<int>("Введите необходимые часы для занятия делом:");
        var neededDays =
            CliHelper.WriteMessageAndTryGetValue<int>("Введите кол-во дней, которое вы хотите заниматься делом:");
        var availableHoursInDay =
            CliHelper.WriteMessageAndTryGetValue<int>("Введите макисмальное кол-во часов для занятия делом в день:");
        var numberOfGenerations =
            CliHelper.WriteMessageAndTryGetValue<int>("Введите кол-во вариантов последовательностей:");
        var calculatedRanges =
            TimeCalculator.CalculateRange(neededHours, neededDays, availableHoursInDay, numberOfGenerations, 1);

        foreach (var calculatedRange in calculatedRanges)
        {
            CliHelper.WriteMessageAndArrayElementsWithSum("Рассчитанный временной диапазон:",
                calculatedRange.ToArray());
        }
    }
}