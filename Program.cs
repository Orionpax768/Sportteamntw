//*******************************************************************************************
//* Практическая работа №15                                                                 *
//* Выполнил: Абдуллаев Э.С., группа 2-ИСПд                                                 *
//* Задание: Написать программу, выполняющую следующие действия:                            *
//*-Ввод с клавиатуры данных в массив, состоящий из 5 элементов типа «Спортивная команда».  *
//*-Вывод на экран информации о всех спортсменах, занимающихся указанным видом спорта       *
//*(вид спорта вводится с клавиатуры).                                                      *
//*-Определить спортсмена с лучшим результатом.                                             *
//*******************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportteam
{
    public struct SportsTeam
    {
        public string FullName;
        public string SportType;
        public double BestResult;
        public bool IsTimeBased;
        public SportsTeam(string _fullName, string _sportType, double _bestResult)
        {
            FullName = _fullName;
            SportType = _sportType;
            BestResult = _bestResult;
            IsTimeBased = false;
        }
    }

    internal class Program
    {
        static bool ContainsDigits(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        static SportsTeam[] InputTeams()
        {
            SportsTeam[] teams = new SportsTeam[5];
            string[] timeBasedSports = { "плавание", "бег", "лыжи", "велоспорт", "гонки", "бассейн" };
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nСпортсмен {i + 1}");
                string fullName;
                do
                {
                    Console.Write($"Фамилия {i + 1}: ");
                    fullName = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(fullName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Фамилия не может быть пустой!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (ContainsDigits(fullName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Фамилия не может содержать цифры!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                } 
                while (String.IsNullOrWhiteSpace(fullName) || ContainsDigits(fullName));
                string sportType;
                do
                {
                    Console.Write($"Вид спорта {i + 1}: ");
                    sportType = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(sportType))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вид спорта не может быть пустым!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (ContainsDigits(sportType))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вид спорта не может содержать цифры!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                } 
                while (String.IsNullOrWhiteSpace(sportType) || ContainsDigits(sportType));
                bool isTimeBased = timeBasedSports.Contains(sportType.ToLower());
                double bestResult;
                while (true)
                {
                    Console.Write($"Лучший результат {i + 1}: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Результат не может быть пустым!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (!Double.TryParse(input, out bestResult))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Введите число!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (bestResult < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Результат не может быть отрицательным!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        break;
                    }
                }
                teams[i] = new SportsTeam(fullName, sportType, bestResult);
                teams[i].IsTimeBased = isTimeBased;
            }
            return teams;
        }

        static void FindAbsoluteBest(SportsTeam[] teams)
        {
            if (teams.Length == 0)
            {
                throw new Exception("Нет данных о спортсменах");
            }
            SportsTeam absoluteBest = teams[0];
            double bestScore = CalculateScore(teams[0]);
            for (int i = 1; i < teams.Length; i++)
            {
                double currentScore = CalculateScore(teams[i]);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    absoluteBest = teams[i];
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n|||||АБСОЛЮТНО ЛУЧШИЙ СПОРТСМЕН|||||");
            Console.WriteLine($"Фамилия: {absoluteBest.FullName}");
            Console.WriteLine($"Вид спорта: {absoluteBest.SportType}");
            Console.WriteLine($"Результат: {absoluteBest.BestResult}");
        }

        static double CalculateScore(SportsTeam team)
        {
            if (team.IsTimeBased)
            {
                return 1000 / team.BestResult;
            }
            else
            {
                return team.BestResult;
            }
        }

        static void FindBestBySport(SportsTeam[] teams)
        {
            if (teams.Length == 0)
            {
                throw new Exception("Нет данных о спортсменах");
            }
            var sportsGroups = teams.GroupBy(t => t.SportType);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n|||ЛУЧШИЕ СПОРТСМЕНЫ ПО ВИДАМ СПОРТА|||");
            foreach (var group in sportsGroups)
            {
                SportsTeam bestInSport = group.First();
                bool isTimeBased = group.First().IsTimeBased;
                foreach (var athlete in group)
                {
                    if ((isTimeBased && athlete.BestResult < bestInSport.BestResult) || (!isTimeBased && athlete.BestResult > bestInSport.BestResult))
                    {
                        bestInSport = athlete;
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\n{group.Key.ToUpper()}:");
                Console.WriteLine($"{bestInSport.FullName}");
                Console.WriteLine($"Результат: {bestInSport.BestResult}");
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.Title = "Практическая работа №13";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Здравствуйте!");
                    Console.WriteLine("Введите данные o 5 спортсменов:");
                    SportsTeam[] teams = InputTeams();
                    FindBestBySport(teams);
                    FindAbsoluteBest(teams);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nВыберите действие:");
                    Console.WriteLine("1 - Новый поиск");
                    Console.WriteLine("0 - Выйти из программы");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Новый поиск...");
                            break;
                        case "0":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Программа завершена.");
                            return;
                        default:
                            throw new Exception("Неверный выбор!");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }
    }
}
