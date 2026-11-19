using DroneControlSystem;
using System;

namespace DroneControlSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления БПЛА (версия 3.0) ===\n");

            // Твой массив из трёх дронов
            Drone[] drones = new Drone[]
            {
                new Drone("DJI Mini 3", 85),
                new Drone("Autel EVO Lite", 45),
                new Drone("Parrot Anafi", 15)
            };

            // Бесконечный цикл меню
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Показать информацию о всех дронах");
                Console.WriteLine("2. Взлёт выбранного дрона");
                Console.WriteLine("3. Подзарядка выбранного дрона");
                Console.WriteLine("4. Расчёт времени полёта");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string input = Console.ReadLine();

                // Проверка, что введено число
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Ошибка: введите число.");
                    continue; // Начинаем цикл заново
                }

                if (choice == 0)
                {
                    Console.WriteLine("Программа завершена.");
                    break; // Выход из программы
                }

                // Для пунктов 2, 3 и 4 нужно знать, с каким дроном работаем
                if (choice >= 2 && choice <= 4)
                {
                    Console.Write("Введите индекс дрона (0, 1 или 2): ");
                    string indexInput = Console.ReadLine();

                    // Проверка правильности индекса
                    if (!int.TryParse(indexInput, out int index) || index < 0 || index >= drones.Length)
                    {
                        Console.WriteLine("Ошибка: неверный индекс дрона.");
                        continue;
                    }

                    Drone selectedDrone = drones[index];

                    // Выполняем выбранное действие
                    if (choice == 2)
                    {
                        selectedDrone.TakeOff();
                    }
                    else if (choice == 3)
                    {
                        Console.Write("На сколько % подзарядить? ");
                        string chargeInput = Console.ReadLine();
                        if (int.TryParse(chargeInput, out int amount))
                        {
                            selectedDrone.ChargeBattery(amount);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите число.");
                        }
                    }
                    else if (choice == 4)
                    {
                        int time = selectedDrone.CalculateFlightTime();
                        if (time > 0) Console.WriteLine($"Время полёта: {time} мин.");
                    }
                }
                else if (choice == 1)
                {
                    // Вывод информации о всех дронах
                    foreach (Drone d in drones)
                    {
                        d.PrintInfo();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Попробуйте снова.");
                }
            }
        }
    }
}