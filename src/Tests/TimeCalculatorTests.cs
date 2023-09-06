namespace Tests;

using FluentAssertions;

using TimeCalculator.Calculators;
using TimeCalculator.Extensions;
using TimeCalculator.Models;

/// <summary>
/// Тесты для <see cref="TimeCalculator"/>.
/// </summary>
public class TimeCalculatorTests
{
    [Theory]
    [InlineData(40, 31,
        2, 0)] // часов меньше максимально возможного заполнения 31*2=62; часов больше 0, доступных часов в день - меньше чем часов в дне, погрешность положительная;
    public void CorrectData_CalculateRanges_Success(int neededHours, int neededDays,
        int availableHoursInDay, int tolerance)
    {
        // Arrange
        var timeCalculationInfo = new TimeCalculationInfo(neededHours, neededDays, availableHoursInDay, tolerance);
        var timeCalculator = new TimeCalculator(timeCalculationInfo);

        // Act
        var calculatedRange = timeCalculator.CalculateRanges(1).First();
        var rangeSumHours = calculatedRange.Sum().ToHours();

        // Assert
        rangeSumHours.Should().BeLessOrEqualTo(neededHours - tolerance);
    }

    [Theory]
    [InlineData(0, 13, 2, 0)] // необходимых часов - 0, нет смысла считать для 0 часов.
    [InlineData(160, 31, 90, 0)] // доступных часов в день - больше чем часов в сутках.
    [InlineData(160, 13, 2, 0)] // часов больше максимально возможного заполнения 13*2=26.
    [InlineData(160, 13, 2, -1)] // погрешность отрицательная.
    [InlineData(0, 0, 0, 0)]
    public void IncorrectData_InitializeTimeCalculationInfo_Fail(int neededHours, int neededDays,
        int availableHoursInDay, int tolerance)
    {
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new TimeCalculationInfo(neededHours, neededDays, availableHoursInDay, tolerance));
    }

    [Theory]
    [InlineData(5)]
    public void CorrectCalculationsCount_CalculateRange_Success(int calculationsCount)
    {
        // Arrange
        var timeCalculationInfo = new TimeCalculationInfo(30, 7, 8);
        var timeCalculator = new TimeCalculator(timeCalculationInfo);

        // Act
        var calculatedRange = timeCalculator.CalculateRanges(calculationsCount);

        // Assert
        calculatedRange.Should().HaveCount(5);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IncorrectCalculationsCount_CalculateRange_Success(int calculationsCount)
    {
        // Arrange
        var timeCalculationInfo = new TimeCalculationInfo(30, 7, 8);
        var timeCalculator = new TimeCalculator(timeCalculationInfo);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => timeCalculator.CalculateRanges(calculationsCount));
    }
}