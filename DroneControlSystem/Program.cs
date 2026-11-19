using DroneControlSystem;
using System;

namespace DroneControlSystem
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Система управления БПЛА (версия 3.0) ===\n");

            // Твой массив из трёх дронов
            Drone[] drones = new Drone[]

            // 1. Создаем дронов
            Drone[] drones =
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

            // 2. Бесконечное меню
            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1 - Инфо о всех дронах");
                Console.WriteLine("2 - Взлет дрона");
                Console.WriteLine("3 - Подзарядка дрона");
                Console.WriteLine("4 - Время полета");
                Console.WriteLine("5 - Сохранить в файл");
                Console.WriteLine("6 - Загрузить из файла");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 0) break; // Выход

                if (choice == 1)
                {
                    foreach (Drone d in drones) d.PrintInfo();
                }
                else if (choice == 5)
                {
                    FileService.SaveDrones(drones, "drones.json");
                }
                else if (choice == 6)
                {
                    drones = FileService.LoadDrones("drones.json");
                    Console.WriteLine("Загружено!");
                }
                else if (choice >= 2 && choice <= 4)
                {
                    // Спрашиваем, с каким дроном работаем
                    Console.Write("Индекс дрона (0, 1, 2): ");
                    int index = int.Parse(Console.ReadLine());
                    Drone d = drones[index];

                    if (choice == 2) d.TakeOff();
                    else if (choice == 3)
                    {
                        Console.Write("На сколько % зарядить? ");
                        d.ChargeBattery(int.Parse(Console.ReadLine()));
                    }
                    else if (choice == 4)
                    {
                        Console.WriteLine($"Время: {d.CalculateFlightTime()} мин.");
                    }
                }
                else
                {
                    Console.WriteLine("Нет такой команды.");
                }
            }
        }
    }
}