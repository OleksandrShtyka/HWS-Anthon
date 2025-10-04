#nullable enable
using System;

namespace SafeDemo
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                // 1) Безопасная обработка отсутствия пользователя
                new Report().Process();                 // Person не задан — выведет предупреждение
                new Report(new Person("Alex")).Process(); // Пример корректного запуска

                // 2) Деление и обход массивов без исключений как логики
                var array = new[] { 10, 20, 10, 20, 50 };
                var anotherArray = new[] { 10, 20, 0, 10, 50, 300 };

                foreach (var item in array)
                {
                    foreach (var anotherItem in anotherArray)
                    {
                        if (anotherItem == 0)
                        {
                            Console.WriteLine("⚠️ Неможливо ділити на нуль!");
                            continue;
                        }

                        int result = item / anotherItem;
                        Console.WriteLine($"Результат ділення {item} на {anotherItem}: {result}");
                    }
                }

                // 3) Безопасный вызов BlackBox без рекурсии
                BlackBox.ShowByeMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Непередбачена помилка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("✅ Програма завершила роботу без аварійного завершення.");
                // Console.ReadKey(); // Открой при запуске из консоли вручную
            }
        }
    }

    internal sealed class Person
    {
        public Person(string name) => Name = name;
        public string Name { get; set; }
    }

    internal sealed class Report
    {
        private readonly Person? _person;

        public Report() : this(null) { }

        public Report(Person? person) => _person = person;

        public void Process()
        {
            // Ранний выход вместо ловли NRE
            if (_person is null || string.IsNullOrWhiteSpace(_person.Name))
            {
                Console.WriteLine("⚠️ Користувач відсутній! Створіть об'єкт Person перед запуском звіту.");
                return;
            }

            Console.WriteLine($"Process for {_person.Name}");
        }
    }

    internal static class BlackBox
    {
        public static void ShowByeMessage() => Console.WriteLine("👋 Bye!");
    }
}
