using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAVTest
{
    public class ThreadSafeQueue<T>
    {
        private readonly SemaphoreSlim popSemaphore = new SemaphoreSlim(0, 1);

        private readonly SemaphoreSlim readWriteSemaphore = new SemaphoreSlim(1, 1);

        private Queue<T> queue = new Queue<T>();

        public void Push(T item)
        {
            readWriteSemaphore.Wait();
            try
            {
                queue.Enqueue(item);

                if (queue.Count == 1) // if it's the first item release the pop handle
                {
                    popSemaphore.Release();
                }
            }
            finally
            {
                readWriteSemaphore.Release();
            }
        }

        public T Pop()
        {
            readWriteSemaphore.Wait();
            try
            {
                if (queue.Count == 0) // wait for new items
                {
                    readWriteSemaphore.Release(); // allow items to be added
                    popSemaphore.Wait(); // TODO: specify a timeout
                }

                if (queue.Count == 1 && popSemaphore.CurrentCount == 1) // if queue is empty reset semaphore to 0
                    popSemaphore.Wait(); // TODO: specify a timeout

                return queue.Dequeue();
            }
            finally
            {
                if (readWriteSemaphore.CurrentCount == 0)
                    readWriteSemaphore.Release();
            }
        }
    }
}
