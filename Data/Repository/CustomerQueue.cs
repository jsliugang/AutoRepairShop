using System;
using System.Collections.Generic;
using System.Xml.Schema;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Repository
{
    class CustomerQueue<T> where T : IComparable<T>
    {
        public static void Enqueue(T item, List<T> currentList)
        {
            currentList.Add(item);
            int ci = currentList.Count - 1;
            while (ci > 0)
            {
                int pi = (ci - 1) / 2;
                if (currentList[ci].CompareTo(currentList[pi]) >= 0)
                    break;
                T tmp = currentList[ci]; currentList[ci] = currentList[pi]; currentList[pi] = tmp;
                ci = pi;
            }
        }

        public static T Dequeue(List<T> currentList)
        {
            if (currentList.Count == 0)
            {
                return default(T);
            }
            int li = currentList.Count - 1;
            T frontItem = currentList[0];
            currentList[0] = currentList[li];
            currentList.RemoveAt(li);

            --li;
            int pi = 0;
            while (true)
            {
                int ci = pi * 2 + 1;
                if (ci > li) break;
                int rc = ci + 1;
                if (rc <= li && currentList[rc].CompareTo(currentList[ci]) < 0)
                    ci = rc;
                if (currentList[pi].CompareTo(currentList[ci]) <= 0) break;
                T tmp = currentList[pi]; currentList[pi] = currentList[ci]; currentList[ci] = tmp;
                pi = ci;
            }
            return frontItem;
        }

        public static T Read(int pos, List<T> currentList)
        {
            if (pos < currentList.Count)
                return currentList[pos];
            return currentList[0];
        }

        public static T Peek(List<T> currentList)
        {
            if (currentList.Count==0)
            {
                return default(T);
            }
            return currentList[0];
        }

        public static bool Empty(List<T> currentList)
        {
            return currentList.Count == 0;
        }

        public static bool Contains(List<T> currentList, T item)
        {
            return currentList.Contains(item);
        }

        public static void Remove(List<T> currentList, T item)
        {
            if (currentList.Contains(item))
            {
                currentList.RemoveAt(currentList.IndexOf(item));
            }
        }

        public static void Display(List<Customer> currentList)
        {
            foreach (Customer item in currentList)
            {
                Console.Write($"Customer in line: {item.Name}, priority - {item.MyDiscounts.Priority} \n");
            }
        }
    }

}
