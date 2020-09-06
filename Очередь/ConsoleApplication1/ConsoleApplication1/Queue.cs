using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ForQueue
    {
        public int data;
        public ForQueue next;

        public ForQueue(int a)
        {
            data = a;
            next = null;
        }
    }

    class MQueue
    {
        public ForQueue head;
        public ForQueue tail;

        public MQueue()
        {
            head = null;
            tail = null;
        }

        public void AddinQueue(int a)//положить элемент в очередь
        {
            if (head == null)
            {
                head = new ForQueue(a);
                tail = head;
            }
            else
            {
                ForQueue temp = new ForQueue(a);
                tail.next = temp;
                tail = temp;
            }
        }

        public string display()
        {
            string str = "";
            if (head != null)
            {
                ForQueue temp = head;
                while (true)
                {
                    str = str + temp.data + " ";
                    if (temp == tail) break;
                    temp = temp.next;
                }
            }
            return str;
        }

        public void First()
        {
            try
            {
                Console.WriteLine("Извлеченный первый элемент: " + head.data);
                Console.Write("Сама очередь: " + display());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public void FirstDel()
        {
            try
            {
                Console.WriteLine("Извлеченный первый элемент: " + head.data);
                if (head == tail)
                    tail = null;
                head = head.next;
                Console.WriteLine("Сама очередь: " + display());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public int searchmin()
        {
            int min = head.data;
            ForQueue temp = head;
            while (true)
            {
                if (min > temp.data)
                {
                    min = temp.data;
                }
                if (temp == tail) break;
                temp = temp.next;
            }
            return min;
        }

        public int searchmax()
        {
            int max = head.data;
            ForQueue temp = head;
            while (true)
            {
                if (max < temp.data)
                {
                    max = temp.data;
                }
                if (temp == tail) break;
                temp = temp.next;
            }
            return max;
        }

        public int count()
        {
            int i = 0;
            ForQueue temp = head;
            if (head != null)
            {
                while (true)
                {
                    i++;
                    if (temp == tail) break;
                    temp = temp.next;
                }
            }
            return i;
        }

        public void Pop()
        {
           ForQueue temp;
           while (head !=null)
            {
                temp = head;
                head = head.next;
                temp = null;
            }
        }
    }
}
