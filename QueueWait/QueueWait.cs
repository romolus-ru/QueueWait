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
        private object _lockerQueue = new object();
        
        /// <summary>
        /// Блокировка ожидания хоть одного элемента
        /// </summary>
        private object _lockerWait = new object();

        /// <summary>
        /// Блокировка для операции получения элемента, что бы ждал только 1 поток, остальные блокировались
        /// </summary>
        private object _lockerPopSingle = new object();

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
                lock (_lockerQueue){
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
            lock (_lockerPopSingle){// в один поток (иначе "список пуст")
                lock (_lockerWait){// с ожиданием 
                    if (_queue.Count == 0) Monitor.Wait(_lockerWait);
                    lock (_lockerQueue){// доступ к очереди
                        if (_queue.Count != 0){
                            ret = _queue.Dequeue();
                        }
                    }
                }
            }
            return ret;
        }

    }
}
