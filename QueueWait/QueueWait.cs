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
        private object _lockerWait = new object();

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
            lock (_lockerWait){
                lock (_locker){
                    _queue.Enqueue(value);
                }
                Monitor.Pulse(_lockerWait);
            }
        }

        /// <summary>
        /// pop
        /// </summary>
        /// <returns></returns>
        public T pop()
        {
            T ret = default(T);
            lock (_lockerWait){
                var found = false;
                while (!found){
                    if (_queue.Count == 0) Monitor.Wait(_lockerWait);
                    // иногда бывает сообщение очередь пуста, поэтому пришлось добавить while и проверку
                    lock (_locker){
                        if (_queue.Count != 0){
                            ret = _queue.Dequeue();
                            found = true;
                        }
                    }
                }
            }
            return ret;
        }

    }
}
