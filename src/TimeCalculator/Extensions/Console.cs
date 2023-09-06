namespace TimeCalculator.Extensions;

using SystemConsole = System.Console;

/// <summary>
/// Обёртка класса <see cref="System.Console"/> для работы с интерфейсом командной строки.
/// </summary>
public static class Console
{
    /// <summary>
    /// Написать сообщение и попытаться получить значение.
    /// </summary>
    /// <param name="message">Сообщение, которое необходимо вывести в командную строку.</param>
    /// <typeparam name="TResult">Тип выходного значения.</typeparam>
    /// <returns>Полученное от пользователя значение и приведенное к требуемому типу.</returns>
    /// <exception cref="NullReferenceException">Когда введенное пользователем значение было пустым.</exception>
    public static TResult WriteMessageAndTryGetValue<TResult>(string message)
    {
        SystemConsole.Write(message);

        var valueString = SystemConsole.ReadLine();
        SystemConsole.WriteLine();

        if (string.IsNullOrEmpty(valueString))
        {
            throw new NullReferenceException("Введенное пользователем значение было пустым.");
        }

        var gotValue = SwitchTypeAndGetValue<TResult>(valueString);

        return gotValue;
    }

    /// <summary>
    /// Определить тип и получить значение строки.
    /// </summary>
    /// <param name="valueString">Строка со значением.</param>
    /// <typeparam name="TResult">Тип выходного значения.</typeparam>
    /// <returns>Значение требуемого типа.</returns>
    /// <exception cref="InvalidOperationException">Когда не удалось привести значение к требуемому типу</exception>
    private static TResult SwitchTypeAndGetValue<TResult>(string valueString)
    {
        switch (Type.GetTypeCode(typeof(TResult)))
        {
            case TypeCode.String:
            {
                return (TResult)(object)valueString;
            }
            case TypeCode.Int32:
            {
                if (int.TryParse(valueString, out var intValue))
                {
                    return (TResult)(object)intValue;
                }

                break;
            }
            case TypeCode.Boolean:
            {
                if (bool.TryParse(valueString, out var boolValue))
                {
                    return (TResult)(object)boolValue;
                }

                break;
            }
        }

        throw new InvalidCastException(
            $"Не удалось привести полученное значение: {valueString}, к требуемому типу: {typeof(TResult)}.");
    }

    /// <summary>
    /// Написать сообщение и элементы массива с их суммой.
    /// </summary>
    /// <param name="message">Сообщение, которое необходимо вывести в командную строку.</param>
    /// <param name="array">Массив, элементы которого необходимо вывести в командную строку.</param>
    public static void WriteMessageAndArrayElementsWithSum(string message, int[] array)
    {
        SystemConsole.WriteLine(message);

        for (var count = 0; count < array.Length; count++)
        {
            SystemConsole.WriteLine($"День {count + 1}. {array[count]} мин.");
        }

        SystemConsole.WriteLine($"Сумма элементов: {array.Sum().ToHours()} ч.");
        SystemConsole.WriteLine();
    }
}