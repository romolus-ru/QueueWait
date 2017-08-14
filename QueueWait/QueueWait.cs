using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueWait
{
    /// <summary>
    /// Очередь с ожиданием при получении
    /// </summary>
    public class QueueWait<T>
    {
        private object _locker = new object();
        private Queue<T> _queue = new Queue<T>();

        /// <summary>
        /// push
        /// </summary>
        /// <param name="value"></param>
        public void push(T value)
        {
            lock (_locker){
                _queue.Enqueue(value);
            }
        }

        /// <summary>
        /// pop
        /// </summary>
        /// <returns></returns>
        public T pop()
        {
            // если нету элементов то ждём
            while (_queue.Count==0){
                Thread.Sleep(20);
            }
            T ret;
            lock (_locker){
                ret = _queue.Dequeue();
            }
            return ret;
        }

    }
}
