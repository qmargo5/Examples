using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MQueue q = new MQueue();
            do
            {
                Console.WriteLine("Выберите дейтвие:\n" +
                    "1)Положить элемент в очередь\n2)Элемент из очереди без удаления\n" +
                    "3)Элемент из очереди с удалением\n4)Вывести min элемент\n" +
                    "5)Вывести max элемент\n6)Вывести кол-во элементов\n7)Очистить\n8)Вывести");

                string ans = Console.ReadLine();
                switch (ans)
                {
                    case "1":
                        Console.Write("Введите значение:");
                        int a = int.Parse(Console.ReadLine());
                        q.AddinQueue(a);
                        Console.Write("Полученная очередь:");
                        Console.WriteLine(q.display());
                        Console.Write("\n");
                        break;
                    case "2":
                        q.First();
                        Console.Write("\n");
                        break;
                    case "3":
                        q.FirstDel();
                        Console.Write("\n");
                        break;
                    case "4":
                        Console.WriteLine(q.searchmin());
                        Console.Write("\n");
                        break;
                    case "5":
                        Console.WriteLine(q.searchmax());
                        Console.Write("\n");
                        break;
                    case "6":
                        Console.WriteLine(q.count());
                        Console.Write("\n");
                        break;
                    case "7":
                        q.Pop();
                        break;
                    case "8":
                        Console.WriteLine(q.display());
                        break;
                }
                    
            }
            while (true);

            Console.WriteLine("Введите 5 элементов для записи в список:");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("Введите " + (i+1) + " элемент: ");
                int a = int.Parse(Console.ReadLine());
                q.AddinQueue(a);
            }
            Console.WriteLine(q.display());
            Console.ReadKey();
        }
    }
}
