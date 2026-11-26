using System;

namespace LAB
{
    internal class Checked
    {
        public int ReadInt(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            int x = 0;
            
            while (true)
            {
                if (int.TryParse(input, out x))
                {
                    return x;
                }
                else
                {
                    Console.Write("Ошибка ввода! Повторите ввод: ");
                    input = Console.ReadLine();
                }
            }
        }
        
        public long ReadLong(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            long x = 0;
            
            while (true)
            {
                if (long.TryParse(input, out x) && x > 0)
                {
                    return x;
                }
                else
                {
                    Console.Write("Ошибка ввода! Повторите ввод: ");
                    input = Console.ReadLine();
                }
            }
        }
        
        public decimal ReadDecimal(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            decimal x = 0;
            
            while (true)
            {
                if (decimal.TryParse(input, out x))
                {
                    return x;
                }
                else
                {
                    Console.Write("Ошибка ввода! Повторите ввод: ");
                    input = Console.ReadLine();
                }
            }
        }
        
        public int NewReadInt(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            int x = 0;
            
            while (true)
            {
                if (int.TryParse(input, out x) && x > 0)
                {
                    return x;
                }
                else
                {
                    Console.Write("Ошибка! Введите целое число больше нуля: ");
                    input = Console.ReadLine();
                }
            }
        }
        
        public int ReadCount(string prompt, int minRequired, int maxRequired)
        {
            while (true)
            {
                int count = ReadInt(prompt);
                if (maxRequired == 0)
                {
                    if (count < minRequired)
                    {
                        Console.WriteLine($"Ошибка, введите значение >= {minRequired}");
                        continue;
                    }
                }
                else
                {
                    if ((count < minRequired) || (count > maxRequired))
                    {
                        Console.WriteLine($"Ошибка, введите значение от {minRequired} до {maxRequired}");
                        continue;
                    }
                }
                
                return count;
            }
        }
        public string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Ошибка, строка не может быть пустой");
            }
        }
    }
}