using System;
using System.Collections.Generic;
using AutoRepairShop.Data.Models.Humans;

namespace AutoRepairShop.Data.Repository
{
    internal class CustomerQueue<T> where T : IComparable<T>
    {

        public static void Enqueue(Customer item, List<Customer> currentList)
        {
            lock (currentList)
            {
                currentList.Add(item);
                var ci = currentList.Count - 1;
                while (ci > 0)
                {
                    var pi = (ci - 1) / 2;
                    if (currentList[ci].CompareTo(currentList[pi]) >= 0)
                        break;
                    var tmp = currentList[ci];
                    currentList[ci] = currentList[pi];
                    currentList[pi] = tmp;
                    ci = pi;
                }
            }
        }

        public static T Dequeue(List<T> currentList)
        {
            lock (currentList)
            {
                if (currentList.Count == 0)
                    return default(T);
                var li = currentList.Count - 1;
                var frontItem = currentList[0];
                currentList[0] = currentList[li];
                currentList.RemoveAt(li);
                --li;
                var pi = 0;
                while (true)
                {
                    var ci = pi * 2 + 1;
                    if (ci > li) break;
                    var rc = ci + 1;
                    if (rc <= li && currentList[rc].CompareTo(currentList[ci]) < 0)
                        ci = rc;
                    if (currentList[pi].CompareTo(currentList[ci]) <= 0) break;
                    var tmp = currentList[pi];
                    currentList[pi] = currentList[ci];
                    currentList[ci] = tmp;
                    pi = ci;
                }
                return frontItem;
            }           
        }

        public static T Read(int pos, List<T> currentList)
        {
            return pos < currentList.Count ? currentList[pos] : currentList[0];
        }

        public static T Peek(List<T> currentList)
        {
            return currentList.Count == 0 ? default(T) : currentList[0];
        }

        public static bool Empty(List<T> currentList)
        {
            lock (currentList)
            {
                return currentList.Count == 0;
            }
        }

        public static bool Contains(List<T> currentList, T item)
        {
            return currentList.Contains(item);
        }

        public static void Remove(List<T> currentList, T item)
        {
            lock (currentList)
            {
                if (currentList.Contains(item))
                    currentList.RemoveAt(currentList.IndexOf(item));
            }
        }

        public static void Display(List<Customer> currentList)
        {
            lock (currentList)
            {
                foreach (var item in currentList)
                    Console.Write($"Customer in line: {item.Name}, priority - {item.MyDiscounts.Priority} \n");
            }
        }
    }
}
