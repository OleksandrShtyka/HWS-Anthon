using System;
using System.Collections.Generic;

namespace ErrorPlayground
{
    class Program
    {
        private static readonly Dictionary<string, (string title, Action run)> Demos = new()
        {
            ["1"] = ("NullReferenceException", () => { object o = null; _ = o.ToString(); }
            ),
            ["2"] = ("IndexOutOfRangeException", () => { var arr = new[] { 1, 2, 3 }; Console.WriteLine(arr[5]); }
            ),
            ["3"] = ("DivideByZeroException", () => { int a = 10, b = 0; Console.WriteLine(a / b); }
            ),
            ["4"] = ("FormatException", () => { var n = int.Parse("не_число"); Console.WriteLine(n); }
            ),
            ["5"] = ("ArgumentOutOfRangeException", () => { var s = "abc"; Console.WriteLine(s.Substring(5)); }
            ),
            ["6"] = ("InvalidOperationException", () => throw new InvalidOperationException("Стан об'єкта не дозволяє дію.")),
            ["7"] = ("StackOverflowException (не виконується)", () => throw new NotSupportedException("StackOverflowException навмисно не відтворюється."))
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Демонстратор помилок .NET ===");
            foreach (var kv in Demos)
                Console.WriteLine($"{kv.Key}) {kv.Value.title}");
            Console.WriteLine("q) Вихід");

            while (true)
            {
                Console.Write("Вибір: ");
                var choice = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
                if (choice == "q") break;

                if (!Demos.TryGetValue(choice, out var demo))
                {
                    Console.WriteLine("Невідомий варіант.\n");
                    continue;
                }

                try
                {
                    demo.run();
                    Console.WriteLine("✅ OK\n");
                }
                catch (DivideByZeroException) { Console.WriteLine("⚠️ DivideByZeroException\n"); }
                catch (IndexOutOfRangeException) { Console.WriteLine("⚠️ IndexOutOfRangeException\n"); }
                catch (NullReferenceException) { Console.WriteLine("⚠️ NullReferenceException\n"); }
                catch (FormatException) { Console.WriteLine("⚠️ FormatException\n"); }
                catch (ArgumentOutOfRangeException) { Console.WriteLine("⚠️ ArgumentOutOfRangeException\n"); }
                catch (InvalidOperationException ex) { Console.WriteLine($"⚠️ InvalidOperationException: {ex.Message}\n"); }
                catch (NotSupportedException ex) { Console.WriteLine($"ℹ️ {ex.Message}\n"); }
                catch (Exception ex) { Console.WriteLine($"⚠️ Непередбачена помилка: {ex.GetType().Name}: {ex.Message}\n"); }
            }
        }
    }
}
