//*******************************************************************************************
//* Практическая работа №15                                                                 *
//* Выполнил: Абдуллаев Э.С., группа 2-ИСПд                                                 *
//* Задание: Написать программу, выполняющую следующие действия:                            *
//* - Ввод с клавиатуры данных в массив, состоящий из 5 элементов типа «Спортивная команда».*
//* - Вывод на экран информации о всех спортсменах, занимающихся указанным видом спорта     *
//*   (вид спорта вводится с клавиатуры).                                                   *
//* - Определить спортсмена с лучшим результатом (среди указанного вида спорта).            *
//*******************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SportTeam
{
    internal class Program
    {
        public struct SportsTeam
        {
            public string FullName;
            public string SportType;
            public double BestResult;

            public SportsTeam(string fullName, string sportType, double bestResult)
            {
                FullName = fullName;
                SportType = sportType;
                BestResult = bestResult;
            }
        }

        static string ReadNonEmptyString(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Значение не может быть пустым или состоять только из пробелов.");
            }
            return input;
        }

        static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!Double.TryParse(input, out double result))
            {
                throw new ArgumentException("Некорректное числовое значение.");
            }
            return result;
        }

        static SportsTeam[] InputTeams(int count)
        {
            SportsTeam[] teams = new SportsTeam[count];
            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"\nВведите данные для спортсмена №{i + 1}:");
                        string fullName = ReadNonEmptyString("Фамилия: ");
                        string sportType = ReadNonEmptyString("Вид спорта: ");
                        double bestResult = ReadDouble("Лучший результат (число): ");
                        teams[i] = new SportsTeam(fullName, sportType, bestResult);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ошибка: {ex.Message}");
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
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
            return teams;
        }

        static void DisplayAthletesBySport(SportsTeam[] teams, string searchSport)
        {
            if (String.IsNullOrWhiteSpace(searchSport))
            {
                throw new ArgumentException("Вид спорта не может быть пустым.", nameof(searchSport));
            }
            bool found = false;
            Console.WriteLine($"\nСпортсмены, занимающиеся видом спорта {searchSport}:");
            foreach (SportsTeam team in teams)
            {
                if (team.SportType.Equals(searchSport, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"ФИО: {team.FullName}, Лучший результат: {team.BestResult}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Спортсмены с указанным видом спорта не найдены.");
            }
        }

        static bool FindBestAthleteInSport(SportsTeam[] teams, string searchSport, out SportsTeam bestAthlete)
        {
            if (teams == null)
                throw new ArgumentNullException("Строка не может быть пустой!");
            if (String.IsNullOrWhiteSpace(searchSport))
                throw new ArgumentException("Вид спорта не может быть пустым!");
            bestAthlete = new SportsTeam();
            bool found = false;
            foreach (SportsTeam team in teams)
            {
                if (team.SportType.Equals(searchSport, StringComparison.OrdinalIgnoreCase))
                {
                    if (!found || team.BestResult > bestAthlete.BestResult)
                    {
                        bestAthlete = team;
                        found = true;
                    }
                }
            }
            return found;
        }

        static void Main(string[] args)
        {
            const int teamCount = 5;
            SportsTeam[] teams = InputTeams(teamCount);
            Console.Write("\nВведите вид спорта для поиска: ");
            string searchSport = Console.ReadLine();
            try
            {
                DisplayAthletesBySport(teams, searchSport);
                if (FindBestAthleteInSport(teams, searchSport, out SportsTeam bestInSport))
                {
                    Console.WriteLine($"Спортсмен с лучшим результатом в виде спорта {searchSport}:");
                    Console.WriteLine($"ФИО: {bestInSport.FullName} " +
                        $"\nВид спорта: {bestInSport.SportType} " +
                        $"\nРезультат: {bestInSport.BestResult}");
                }
                else if (!String.IsNullOrWhiteSpace(searchSport))
                {
                    Console.WriteLine("\nНевозможно определить лучшего спортсмена, так как никто не занимается спорта.");
                }
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Новый поиск...");
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Программа завершена.");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("Неверный выбор!");
                }
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
