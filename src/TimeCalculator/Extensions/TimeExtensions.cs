namespace TimeCalculator.Extensions;

/// <summary>
/// Расширения для работы со временем.
/// </summary>
public static class TimeExtensions
{
    /// <summary>
    /// Перевести часы в минуты.
    /// </summary>
    /// <param name="hours">Часы.</param>
    /// <typeparam name="TNumber">Тип переменной часов.</typeparam>
    /// <returns>Минуты.</returns>
    /// <exception cref="InvalidCastException">Когда были переданы часы с неподдерживаемым типом для перевода.</exception>
    public static int ToMinutes<TNumber>(this TNumber hours) where TNumber : struct
    {
        return hours switch
        {
            int intHours => intHours * 60,
            double doubleHours => (int)(doubleHours * 60),
            _ => throw new InvalidCastException($"Тип \"{nameof(TNumber)}\" не поддерживается для перевода в минуты.")
        };
    }

    /// <summary>
    /// Перевести минуты в часы.
    /// </summary>
    /// <param name="minutes">Минуты.</param>
    /// <typeparam name="TNumber">Тип переменной минут.</typeparam>
    /// <returns>Часы.</returns>
    /// <exception cref="InvalidCastException">Когда были переданы минуты с неподдерживаемым типом для перевода.</exception>
    public static int ToHours<TNumber>(this TNumber minutes) where TNumber : struct
    {
        return minutes switch
        {
            int intMinutes => intMinutes / 60,
            double doubleMinutes => (int)(doubleMinutes / 60),
            _ => throw new InvalidCastException($"Тип \"{nameof(TNumber)}\" не поддерживается для перевода в часы.")
        };
    }

    /// <summary>
    /// Вывести рассчитанные временные диапазоны.
    /// </summary>
    /// <param name="timesRanges">Рассчитанные временные диапазоны</param>
    public static void PrintTimes(this IEnumerable<IEnumerable<int>> timesRanges)
    {
        var valueIndexPairs = timesRanges.Select((value, index) => new
        {
            TimeRange = value, Index = index
        });

        foreach (var valueIndexPair in valueIndexPairs)
        {
            Console.WriteMessageAndArrayElementsWithSum($"Рассчитанный временной диапазон №{valueIndexPair.Index + 1}:",
                valueIndexPair.TimeRange.ToArray());
        }
    }
}