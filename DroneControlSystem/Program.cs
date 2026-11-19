using DroneControlSystem;
using System;

namespace DroneControlSystem
{
    class Program
    {
        static void Main()
        {
            // 1. Создаем дронов
            Drone[] drones =
            {
                new Drone("DJI Mini 3", 85),
                new Drone("Autel EVO Lite", 45),
                new Drone("Parrot Anafi", 15)
            };

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