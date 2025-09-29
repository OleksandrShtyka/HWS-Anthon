from future import annotations
from dataclasses import dataclass

@dataclass
class Person:
    name: str
    def post_init(self) -> None:
        if not isinstance(self.name, str): raise TypeError("Ім'я має бути рядком.")
        self.name = self.name.strip()
        if not self.name: raise ValueError("Ім'я не може бути порожнім.")

class BlackBox:
    @staticmethod
    def msg(): print("Продовжуємо...")
    @staticmethod
    def bye(): print("Готово.")

class Report:
    def init(self, person: Person | None = None) -> None:
        try: self._person = person or Person("Гість")
        except (TypeError, ValueError): print("⚠️ Некоректне ім'я, використовую 'Гість'"); self._person = Person("Гість")

    def process(self, data: list[int], divisors: list[int]) -> None:
        try: print(f"Process for {self._person.name}")
        except AttributeError: print("⚠️ Пошкоджений профіль, 'Гість'"); self._person = Person("Гість")
        BlackBox.msg()
        for i in range(len(data)):
            try: a = data[i]
            except IndexError: print("⚠️ Вихід за межі data"); continue
            for j in range(len(divisors)):
                try:
                    b = divisors[j]
                    print(f"{a} / {b} = {a / b}")
                except ZeroDivisionError: print("❌ Ділення на нуль"); continue
                except TypeError:        print("❌ Некоректний тип");  continue
            try: _ = data[i]
            except IndexError: print("⚠️ Індекс помилковий"); continue
        BlackBox.bye()

def main() -> None:
    data = [10, 20, 10, 20, 50]
    divisors = [10, 20, 0, 10, 50, 300]
    try:
        Report(Person("Антон")).process(data, divisors)
    except KeyboardInterrupt: print("\n⏹️ Перервано користувачем")
    except Exception as ex:   print(f"🚧 Неочікувана помилка: {ex}")

if __name__ == "__main__":
    main()