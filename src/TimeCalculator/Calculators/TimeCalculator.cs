namespace TimeCalculator.Calculators;

/// <summary>
/// Калькулятор времени.
/// </summary>
public static class TimeCalculator
{
    /// <summary>
    /// Рассчитать диапазон.
    /// </summary>
    /// <param name="neededHours">Необходимые часы.</param>
    /// <param name="neededDays">Колличество дней.</param>
    /// <param name="availableHoursInDay">Макисмальное доступное колличество часов в день.</param>
    /// <param name="tolerance">Погрешность.</param>
    /// <returns>Рассчитанный диапазон часов.</returns>
    /// <exception cref="ArgumentException">Когда погрешность была меньше 1.</exception>
    public static IEnumerable<int> CalculateRange(int neededHours, int neededDays, int availableHoursInDay,
        int tolerance = 1)
    {
        if (tolerance < 1)
        {
            throw new ArgumentException("Погрешность не может быть меньше 1.");
        }

        var randomMinutes = new Random();

        var randomRange = new int[neededDays];
        while (randomRange.Sum() / 60 < neededHours - 1)
        {
            for (var count = 0; count < randomRange.Length; count++)
            {
                if (randomRange.Sum() / 60 > neededHours - tolerance && randomRange.Length > 0)
                {
                    break;
                }

                int estimatedMinutes;
                do
                {
                    var isRandomDay = randomMinutes.Next(0, 2) is 1 ? false : true;
                    var generatedMinutes = double.MaxValue;
                    while (generatedMinutes > availableHoursInDay)
                    {
                        generatedMinutes = Math.Round(randomMinutes.NextDouble(), 1);

                        if (isRandomDay)
                        {
                            var randomizedMinutes = Math.Round(randomMinutes.NextDouble(), 1);

                            while (generatedMinutes + randomizedMinutes > availableHoursInDay)
                            {
                                randomizedMinutes = Math.Round(randomMinutes.NextDouble(), 1);
                            }

                            generatedMinutes += randomizedMinutes;
                        }
                    }

                    estimatedMinutes = (int)(60 * generatedMinutes);
                }
                while (randomRange[count] + estimatedMinutes > availableHoursInDay * 60);

                randomRange[count] += estimatedMinutes;
            }
        }

        return randomRange;
    }

    /// <summary>
    /// Рассчитать диапазон.
    /// </summary>
    /// <param name="neededHours">Необходимые часы.</param>
    /// <param name="neededDays">Колличество дней.</param>
    /// <param name="availableHoursInDay">Макисмальное доступное колличество часов в день.</param>
    /// <param name="countOfGenerations">Колличество необходимых рассчитанных диапазонов часов.</param>
    /// <param name="tolerance">Погрешность.</param>
    /// <returns>Перечисление рассчитанных диапазонов часов.</returns>
    /// <exception cref="ArgumentNullException">Когда произошла ошибка при создании списка расчетов.</exception>
    public static IEnumerable<IEnumerable<int>> CalculateRange(int neededHours, int neededDays, int availableHoursInDay,
        int countOfGenerations, int tolerance = 1)
    {
        var calculatedRanges = new List<IEnumerable<int>>(countOfGenerations);
        if (calculatedRanges == null)
        {
            throw new ArgumentNullException("Ошибка при создании списка расчетов.");
        }

        for (var generationNumber = 0; generationNumber < countOfGenerations; generationNumber++)
        {
            var calculatedRange = CalculateRange(neededHours, neededDays, availableHoursInDay, tolerance);
            calculatedRanges.Add(calculatedRange);
        }

        return calculatedRanges;
    }
}