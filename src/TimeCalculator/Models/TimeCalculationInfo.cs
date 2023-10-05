namespace TimeCalculator.Models;

/// <summary>
/// Информация для расчетов времени.
/// </summary>
public class TimeCalculationInfo
{
    /// <inheritdoc cref="TimeCalculationInfo"/>
    /// <param name="neededHours">Необходимые часы.</param>
    /// <param name="neededDays">Необходимое количество дней.</param>
    /// <param name="availableHoursInDay">Макисмальное доступное количество часов в день.</param>
    /// <param name="tolerance">Погрешность.</param>
    /// <exception cref="ArgumentOutOfRangeException">Когда были переданы не корректные параметры.</exception>
    public TimeCalculationInfo(int neededHours, int neededDays, int availableHoursInDay, int tolerance = 0)
    {
        if (tolerance < 0)
        {
            throw new ArgumentOutOfRangeException("Погрешность не может быть отрицательной.");
        }

        if (tolerance > neededHours)
        {
            throw new ArgumentOutOfRangeException("Погрешность не может быть равной требуемым часам.");
        }

        if (neededHours <= 0)
        {
            throw new ArgumentOutOfRangeException("Диапазон для 0 часов будет - 0.");
        }

        if (availableHoursInDay > 24)
        {
            throw new ArgumentOutOfRangeException("В сутках не может быть больше 24 часов.");
        }

        if (availableHoursInDay < 1)
        {
            throw new ArgumentOutOfRangeException("Для расчетов необходим хотя бы 1 свободный час в день.");
        }

        if (neededHours > availableHoursInDay * neededDays)
        {
            throw new ArgumentOutOfRangeException(
                "Нельзя расчитать часы т.к. они привышают кол-во максимально доспупные часы за указанный промежуток..");
        }

        NeededHours = neededHours;
        NeededDays = neededDays;
        AvailableHoursInDay = availableHoursInDay;
        Tolerance = tolerance;
    }

    /// <summary>
    /// Необходимые часы.
    /// </summary>
    public int NeededHours { get; set; }

    /// <summary>
    /// Необходимое количество дней.
    /// </summary>
    public int NeededDays { get; set; }

    /// <summary>
    /// Макисмальное доступное количество часов в день.
    /// </summary>
    public int AvailableHoursInDay { get; set; }

    /// <summary>
    /// Погрешность.
    /// </summary>
    public int Tolerance { get; set; }
}