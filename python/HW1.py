from __future__ import annotations
from dataclasses import dataclass
from typing import Iterable


@dataclass
class Person:
    name: str

    def __post_init__(self) -> None:
        # Валідація імені: повинен бути рядком і не порожнім після strip
        if not isinstance(self.name, str):
            raise TypeError("Ім'я має бути рядком.")
        self.name = self.name.strip()
        if not self.name:
            raise ValueError("Ім'я не може бути порожнім.")


class BlackBox:
    @staticmethod
    def msg() -> None:
        print("Продовжуємо...")

    @staticmethod
    def bye() -> None:
        print("Готово.")


class Report:
    def __init__(self, person: Person | str | None = None) -> None:
        # Ініціалізація Report з об'єктом Person або ім'ям.
        if person is None:
            self._person = Person("Гість")
            return

        if isinstance(person, Person):
            self._person = person
            return

        # Якщо передали рядок, спробуємо створити Person — обробляємо помилки специфічно
        try:
            self._person = Person(person)  # може кинути TypeError або ValueError
        except TypeError:
            print("⚠️ Некоректний тип для імені; використовую 'Гість'")
            self._person = Person("Гість")
        except ValueError:
            print("⚠️ Порожнє ім'я; використовую 'Гість'")
            self._person = Person("Гість")

    def process(self, data: Iterable, divisors: Iterable) -> None:
        # Просте валідування, що передані ітеровані колекції (не рядки)
        try:
            if isinstance(data, (str, bytes)) or not hasattr(data, "__iter__"):
                raise TypeError("data має бути колекцією чисел (не рядок).")
            if isinstance(divisors, (str, bytes)) or not hasattr(divisors, "__iter__"):
                raise TypeError("divisors має бути колекцією чисел (не рядок).")
        except TypeError as ex:
            print(f"❗ Помилка вхідних даних: {ex}")
            return

        try:
            print(f"Process for {self._person.name}")
        except AttributeError:
            # Якщо _person не існує — відновлюємо за замовчуванням
            print("⚠️ Пошкоджений профіль; використовую 'Гість'")
            self._person = Person("Гість")
            print(f"Process for {self._person.name}")

        BlackBox.msg()

        for a in data:
            # Окремо ловимо помилки типу для елементів data
            try:
                if not isinstance(a, (int, float)):
                    raise TypeError(f"Елемент data не є числом: {a!r}")
            except TypeError as ex:
                print(f"❌ Некоректний елемент у data: {ex} — пропускаю")
                continue

            for b in divisors:
                try:
                    if not isinstance(b, (int, float)):
                        raise TypeError(f"Елемент divisors не є числом: {b!r}")
                    # Перевірка на нуль окремим блоком
                    if b == 0:
                        raise ZeroDivisionError("Ділення на нуль")
                    result = a / b
                except ZeroDivisionError:
                    print(f"❌ Ділення на нуль: {a} / {b} — пропускаю")
                    continue
                except TypeError as ex:
                    print(f"❌ Некоректний тип у divisors: {ex} — пропускаю")
                    continue
                else:
                    print(f"{a} / {b} = {result}")

        BlackBox.bye()


def main() -> None:
    data = [10, 20, 10, 20, 50]
    divisors = [10, 20, 0, 10, 50, 300]

    try:
        report = Report(Person("Антон"))
        report.process(data, divisors)
    except KeyboardInterrupt:
        print("\n⏹️ Перервано користувачем")
    except TypeError as ex:
        # Специфічний обробник для TypeError
        print(f"🚫 TypeError: {ex}")
    except ValueError as ex:
        # Специфічний обробник для ValueError
        print(f"🚫 ValueError: {ex}")
    except Exception as ex:
        # Загальний обробник — остання інстанція
        print(f"🚧 Неочікувана помилка: {ex}")


if __name__ == "__main__":
    main()
    
    def run_gui():
        try:
            import tkinter as tk
            from tkinter import messagebox
        except Exception as ex:
            print("❗ Не вдалося завантажити tkinter:", ex)
            return

        def on_run():
            name = name_entry.get().strip()
            data_str = data_entry.get().strip()
            divisors_str = divisors_entry.get().strip()

            try:
                data = [float(x) for x in data_str.split(",") if x.strip()]
                divisors = [float(x) for x in divisors_str.split(",") if x.strip()]
            except ValueError:
                messagebox.showerror("Помилка", "Введіть числа через кому.")
                return

            try:
                report = Report(name)
                report.process(data, divisors)
            except Exception as ex:
                messagebox.showerror("Помилка", str(ex))

        root = tk.Tk()
        root.title("Звіт")

        tk.Label(root, text="Ім'я:").grid(row=0, column=0, sticky="e")
        name_entry = tk.Entry(root)
        name_entry.grid(row=0, column=1)

        tk.Label(root, text="Дані (через кому):").grid(row=1, column=0, sticky="e")
        data_entry = tk.Entry(root)
        data_entry.grid(row=1, column=1)

        tk.Label(root, text="Дільники (через кому):").grid(row=2, column=0, sticky="e")
        divisors_entry = tk.Entry(root)
        divisors_entry.grid(row=2, column=1)

        run_btn = tk.Button(root, text="Запустити", command=on_run)
        run_btn.grid(row=3, column=0, columnspan=2, pady=10)

        root.mainloop()

##! Явна помилка ініціалізації всього коду працювати не буде
# if name == "main":
    # main()