namespace TimeCalculator.Calculators;

using global::TimeCalculator.Extensions;
using global::TimeCalculator.Models;

/// <summary>
/// Калькулятор времени.
/// </summary>
public class TimeCalculator
{
    /// <inheritdoc cref="TimeCalculationInfo"/>
    private readonly TimeCalculationInfo _timeCalculationInfo;

    /// <inheritdoc cref="TimeCalculator"/>
    public TimeCalculator(TimeCalculationInfo timeCalculationInfo)
    {
        if (timeCalculationInfo is null)
        {
            throw new NullReferenceException(
                "Для калькулятора времени была передана пустая информация для рассчетов времени.");
        }

        _timeCalculationInfo = timeCalculationInfo;
    }

    /// <summary>
    /// Рассчитать диапазон часов.
    /// </summary>
    /// <returns>Рассчитанный диапазон часов.</returns>
    private IEnumerable<int> CalculateRange()
    {
        var calculatedDays = new int[_timeCalculationInfo.NeededDays];
        var totalCalculatedHours = 0;
        var maxDayMinutes = _timeCalculationInfo.AvailableHoursInDay.ToMinutes();
        var timeRandom = new Random();

        while (totalCalculatedHours < _timeCalculationInfo.NeededHours - _timeCalculationInfo.Tolerance)
        {
            for (var index = 0;
                 index < calculatedDays.Length &&
                 totalCalculatedHours < _timeCalculationInfo.NeededHours - _timeCalculationInfo.Tolerance;
                 index++)
            {
                int calculatedMinutes;
                do
                {
                    calculatedMinutes = timeRandom.NextMinutes(_timeCalculationInfo.AvailableHoursInDay.ToMinutes());
                }
                while (calculatedDays[index] + calculatedMinutes > maxDayMinutes);

                calculatedDays[index] += calculatedMinutes;
                totalCalculatedHours = calculatedDays.Sum().ToHours();
            }
        }

        return calculatedDays;
    }

    /// <summary>
    /// Рассчитать диапазоны часов.
    /// </summary>
    /// <param name="countOfGenerations">Колличество необходимых рассчитанных диапазонов часов.</param>
    /// <returns>Перечисление рассчитанных диапазонов часов.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Когда произошла ошибка при создании списка расчетов.</exception>
    public IEnumerable<IEnumerable<int>> CalculateRanges(int countOfGenerations)
    {
        if (countOfGenerations <= 0)
        {
            throw new ArgumentOutOfRangeException(
                "Колличество необходимых рассчитанных диапазонов часов не можеть быть меньше или равно нулю.");
        }

        var calculatedRanges = new List<IEnumerable<int>>(countOfGenerations);
        for (var generationNumber = 0; generationNumber < countOfGenerations; generationNumber++)
        {
            var calculatedRange = CalculateRange();
            calculatedRanges.Add(calculatedRange);
        }

        return calculatedRanges;
    }
}