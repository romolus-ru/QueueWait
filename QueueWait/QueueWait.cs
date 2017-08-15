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
            T ret = default(T);
            var founded = false;
            while (!founded){
                lock (_locker){
                    if (_queue.Count > 0){// блокируем и проверяем на наличие элементов
                        ret = _queue.Dequeue();// сохраняем элемент
                        founded = true;// выходим из цикла
                    }
                }
                if (!founded)
                    Thread.Sleep(20);// если нету элементов то ждём
            }
            return ret;
        }

    }
}
