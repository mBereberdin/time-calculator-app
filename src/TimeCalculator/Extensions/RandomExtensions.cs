namespace TimeCalculator.Extensions;

/// <summary>
/// Расширения для <see cref="Random"/>. 
/// </summary>
public static class RandomExtensions
{
    /// <summary>
    /// Возвращает случайные минуты которые меньше лимита.
    /// </summary>
    /// <param name="timeRandom">Рандомайзер времени.</param>
    /// <param name="limit">Лимит минут.</param>
    /// <returns>Рассчитанные минуты.</returns>
    public static int NextMinutes(this Random timeRandom, int limit)
    {
        int calculatedMinutes;
        do
        {
            var doubleHours = Math.Round(timeRandom.NextDouble(), 1);
            calculatedMinutes = doubleHours.ToMinutes();
        }
        while (calculatedMinutes > limit);

        return calculatedMinutes;
    }
}