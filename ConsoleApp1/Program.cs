using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. feladat
            List<int> numbers = File.ReadAllLines("melyseg.txt").Select(x => int.Parse(x)).ToList();
            Console.WriteLine("1. feladat");
            Console.WriteLine($"A fájl adatainak száma: {numbers.Count()}\n");

            // 2. feladat
            Console.WriteLine("2. feladat");
            Console.Write("Adjon meg egy távolságértéket! ");
            int distance = int.Parse(Console.ReadLine());
            Console.WriteLine($"Ezen a helyen a felszín {numbers[distance - 1]} méter mélyen van.\n");
            Console.Write(numbers[distance-1] != 0 ? "Az adott helyen nincs gödör.\n" : "\n");
            bool isHole = numbers[distance - 1] != 0;

            // 3. feladat
            Console.WriteLine("3. feladat");
            double numberOfZeros = numbers.Where(x => x == 0).Count();
            Console.WriteLine($"Az érintetlen terület aránya: {Math.Round((numberOfZeros / numbers.Count())*100, 2)}%.\n");

            // 4. feladat
            StreamWriter sw = new StreamWriter("godrok.txt");

            bool isZero = false;
            for(int i = 1; i < numbers.Count()-1; i++)
            {
                if (!isZero)
                {
                    if (numbers[i] == 0)
                    {
                        sw.Write("\n");
                        isZero = true;
                    }
                    else
                    {
                        sw.Write($"{numbers[i]} ");
                    }
                } else
                {
                    if(numbers[i] != 0)
                    {
                        isZero = false;
                        sw.Write($"{numbers[i]} ");
                    }
                }
            }

            sw.Close();

            // 5. feladat
            Console.WriteLine("5. feladat");
            int numberOfHoles = 0;
            isZero = false;
            for (int i = 1; i < numbers.Count()-1; i++)
            {
                if(!isZero && numbers[i] == 0)
                {
                    numberOfHoles++;
                    isZero = true;
                }
                else if(isZero && numbers[i] != 0)
                {
                    isZero = false;
                }
            }
            Console.WriteLine($"A gödrök száma: {numberOfHoles-1}\n");

            // 6. feladat
            Console.WriteLine("6. feladat");
            if (isHole)
            {
                Console.WriteLine("a)");
                int position = distance - 1;
                while (numbers[position] > 0)
                {
                    position--;
                }
                int start = position + 2;
                position = distance;
                while (position < numbers.Count() && numbers[position] > 0)
                {
                    position++;
                }
                int end = position;
                Console.WriteLine($"The start of the pit is {start} meters, and the end is {end} meters.");

                Console.WriteLine("b)");
                int lowPoint = 0;
                position = start;
                while (position < end && numbers[position] >= numbers[position - 1])
                {
                    position++;
                }
                while (position < end && numbers[position] <= numbers[position - 1])
                {
                    position++;
                }
                if (position > end)
                {
                    Console.WriteLine("Folyamatosan mélyül.");
                }
                else
                {
                    Console.WriteLine("Nem mélyül folyamatosan.");
                }

                Console.WriteLine("c)");
                int maxDepth = 0;
                for (int i = start - 1; i < end; i++)
                {
                    maxDepth = Math.Max(maxDepth, numbers[i]);
                }
                Console.WriteLine($"A legnagyobb mélység {maxDepth} méter.");

                int volume = 0;
                for (int i = start - 1; i < end; i++)
                {
                    volume += numbers[i];
                }
                volume *= 10;
                Console.WriteLine("d)");
                Console.WriteLine($"A gödör térfogata {volume} m^3.");

                int safeVolume = volume - 10 * (end - start + 1);
                Console.WriteLine("e)");
                Console.WriteLine($"A vízmennyiség {safeVolume} m^3.");
            }

        }
    }
}
