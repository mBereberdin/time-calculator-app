using TimeCalculator;
using TimeCalculator.Workers;

AppConfigurator.AddUnhandledExceptionHandler();
var appWorker = new AppWorker();

while (true)
{
    Console.Clear();
    appWorker.DoWork();
    Console.ReadLine();
}