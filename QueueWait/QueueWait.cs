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
        /// <summary>
        /// Основная блокировка
        /// </summary>
        private object _locker = new object();
        
        /// <summary>
        /// Блокировка ожидания хоть одного элемента
        /// </summary>
        private object _lockerForEmpty = new object();

        private Queue<T> _queue = new Queue<T>();

        public QueueWait()
        {
            //Monitor.Enter(_lockerForEmpty);
        }

        /// <summary>
        /// push
        /// </summary>
        /// <param name="value"></param>
        public void push(T value)
        {
            lock (_lockerForEmpty){
                lock (_locker){
                    _queue.Enqueue(value);
                }
                Monitor.Pulse(_lockerForEmpty);
            }
        }

        /// <summary>
        /// pop
        /// </summary>
        /// <returns></returns>
        public T pop()
        {
            T ret;
            lock (_lockerForEmpty){
                Monitor.Wait(_lockerForEmpty);
                lock (_locker)
                    ret = _queue.Dequeue();
            }
            return ret;
        }

    }
}
