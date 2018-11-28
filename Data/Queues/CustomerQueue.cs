using System;
using System.Collections.Generic;

namespace AutoRepairShop.Data.Queues
{
    class CustomerQueue<T> where T : IComparable<T>
    {
        private List<T> _customers;

        public CustomerQueue()
        {
            _customers = new List<T>();
        }

        public void Enqueue(T item)
        {
            _customers.Add(item);
            int ci = _customers.Count - 1;
            while (ci > 0)
            {
                int pi = (ci - 1) / 2;
                if (_customers[ci].CompareTo(_customers[pi]) >= 0)
                    break;
                T tmp = _customers[ci]; _customers[ci] = _customers[pi]; _customers[pi] = tmp;
                ci = pi;
            }
        }

        public T Dequeue()
        {
            // Assumes pq isn't empty
            int li = _customers.Count - 1;
            T frontItem = _customers[0];
            _customers[0] = _customers[li];
            _customers.RemoveAt(li);

            --li;
            int pi = 0;
            while (true)
            {
                int ci = pi * 2 + 1;
                if (ci > li) break;
                int rc = ci + 1;
                if (rc <= li && _customers[rc].CompareTo(_customers[ci]) < 0)
                    ci = rc;
                if (_customers[pi].CompareTo(_customers[ci]) <= 0) break;
                T tmp = _customers[pi]; _customers[pi] = _customers[ci]; _customers[ci] = tmp;
                pi = ci;
            }
            return frontItem;
        }

        public T Pop()
        {
            return _customers[0];
        }
    }

}
