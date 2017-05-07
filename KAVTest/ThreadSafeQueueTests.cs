using System.Threading.Tasks;
using Xunit;

namespace KAVTest
{
    public class ThreadSafeQueueTests
    {
        [Fact]
        public async void IsThreadSafe()
        {
            var queue = new ThreadSafeQueue<decimal>();

            decimal result = 0;
            var popTask = Task.Factory.StartNew(() => { result = queue.Pop(); });
            await Task.Delay(500);
            Assert.Equal(0, result);
            await Task.Factory.StartNew(() => { queue.Push(10M); });
            await popTask;
            Assert.Equal(10, result);
        }

        [Fact]
        public async void CanPopAndPushLastElement()
        {
            var queue = new ThreadSafeQueue<decimal>();

            decimal result = 0;
            var popTask = Task.Factory.StartNew(() => { result = queue.Pop(); });
            await Task.Delay(500);
            Assert.Equal(0, result);
            await Task.Factory.StartNew(() => { queue.Push(10M); });
            await Task.Factory.StartNew(() => { queue.Push(30M); });
            await Task.Factory.StartNew(() => { queue.Push(50M); });
            await popTask;
            Assert.Equal(10, result);

            popTask = Task.Factory.StartNew(() => { result = queue.Pop(); });
            await popTask;
            Assert.Equal(30, result);

            popTask = Task.Factory.StartNew(() => { result = queue.Pop(); });
            await popTask;
            Assert.Equal(50, result);

            result = 0M;

            popTask = Task.Factory.StartNew(() => { result = queue.Pop(); });
            await Task.Delay(500);
            Assert.Equal(0M, result);

            await Task.Factory.StartNew(() => { queue.Push(55M); });
            await popTask;
            Assert.Equal(55, result);
        }
    }
}