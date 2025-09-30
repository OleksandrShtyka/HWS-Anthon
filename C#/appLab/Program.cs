using System;

class ErrorsModule
{
    class Person
    {
        public Person(string name) => Name = name;
        public string Name { get; set; }
    }

    class Report
    {
        private readonly Person _person;
        public Report() : this(null) { }
        public Report(Person person) => _person = person;

        public void Process()
        {
            try
            {
                // Тут раніше ловили NullReferenceException.
                // Тепер — безпечний доступ і зрозуміле повідомлення.
                if (_person is null || string.IsNullOrWhiteSpace(_person.Name))
                {
                    Console.WriteLine("⚠️ Користувач відсутній! Створіть об'єкт Person перед запуском звіту.");
                    return;
                }
                Console.WriteLine($"Process for {_person.Name}");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("⚠️ Дані користувача пошкоджені. Спробуйте ще раз.");
            }
        }
    }

    static void Main()
    {
        try
        {
            // 1) NullReference / безпечна обробка
            new Report().Process();

            // 2) Індекси та ділення
            var array = new[] { 10, 20, 10, 20, 50 };
            var anotherArray = new[] { 10, 20, 0, 10, 50, 300 };

            for (int i = 0; i < array.Length; i++)               // виправлено <= на <
            {
                int item;
                try
                {
                    item = array[i];
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("⚠️ Вихід за межі масиву у зовнішньому циклі.");
                    continue;
                }

                for (int j = 0; j < anotherArray.Length; j++)    // виправлено <= на <
                {
                    try
                    {
                        int anotherItem = anotherArray[j];
                        int result = item / anotherItem;         // може кинути DivideByZeroException
                        Console.WriteLine($"Результат ділення {item} на {anotherItem}: {result}");
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("⚠️ Неможливо ділити на нуль!");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("⚠️ Ви звернулися до елемента масиву, якого не існує.");
                    }
                }
            }

            // 3) Замість некерованої рекурсії — безпечний варіант без StackOverflow
            try
            {
                BlackBox.ShowByeMessage();
            }
            catch (Exception ex)
            {
                // залишимо загальне перехоплення на випадок неочікуваних помилок
                Console.WriteLine($"⚠️ Непередбачена помилка в BlackBox: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Непередбачена помилка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("✅ Програма завершила роботу без аварійного завершення.");
            Console.ReadKey();
        }
    }

    class BlackBox
    {
        // Безпечна версія: без взаємної рекурсії
        public static void ShowByeMessage()
        {
            Console.WriteLine("👋 Bye!");
        }
    }
}
