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
    struct SportsTeam
    {
        public string FullName;
        public string SportType;
        public double BestResult;
        public bool IsTimeBased;
    }
    internal class Program
    {
        static SportsTeam[] InputTeams()
        {
            SportsTeam[] teams = new SportsTeam[5];
            string[] timeBasedSports = { "плавание", "бег", "лыжи", "велоспорт", "гонки" };
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nСпортсмен {i + 1}");
                Console.Write($"Фамилия {i + 1}: ");
                teams[i].FullName = Console.ReadLine();
                Console.Write($"Вид спорта {i + 1}: ");
                teams[i].SportType = Console.ReadLine();
                teams[i].IsTimeBased = timeBasedSports.Contains(teams[i].SportType.ToLower());
                Console.Write($"Лучший результат {i + 1}: ");
                string input = Console.ReadLine();
                while (!double.TryParse(input, out teams[i].BestResult) || teams[i].BestResult < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ошибка! Введите корректный результат(число должно быть =>0): ");
                    input = Console.ReadLine();
                }
            }
            return teams;
        }

        static void FindAbsoluteBest(SportsTeam[] teams)
        {
            if (teams.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нет данных о спортсменах");
                return;
            }
            SportsTeam absoluteBest = teams[0];
            double bestScore = CalculateScore(teams[0]);
            foreach (var team in teams)
            {
                double currentScore = CalculateScore(team);
                if (currentScore > bestScore)
                {
                    absoluteBest = team;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n===АБСОЛЮТНО ЛУЧШИЙ СПОРТСМЕН===");
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нет данных о спортсменах");
                return;
            }
            var sportsGroups = teams.GroupBy(t => t.SportType);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n---ЛУЧШИЕ СПОРТСМЕНЫ ПО ВИДАМ СПОРТА---");
            foreach (var group in sportsGroups)
            {
                SportsTeam bestInSport;
                if (group.First().IsTimeBased)
                {
                    bestInSport = group.OrderBy(t => t.BestResult).First();
                }
                else
                {
                    bestInSport = group.OrderByDescending(t => t.BestResult).First();
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\n {group.Key.ToUpper()}:");
                Console.WriteLine($" {bestInSport.FullName}");
                Console.WriteLine($" Результат: {bestInSport.BestResult}");
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.Title = "Практическая работа №13";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Здравствуйте!");
                Console.WriteLine("Введите данные 5 спортсменов:");
                SportsTeam[] teams = InputTeams();
                FindBestBySport(teams);  
                FindAbsoluteBest(teams); 
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Новый поиск");
                Console.WriteLine("0 - Выйти из программы");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                Console.ResetColor();
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
                        Console.ReadKey();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверный выбор! Программа будет продолжена.");
                        Console.ReadKey();
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
