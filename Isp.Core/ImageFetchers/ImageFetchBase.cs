using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Isp.Core.Entities;

namespace Isp.Core.ImageFetchers
{
    /// <summary>
    /// Generic class to be inherited and implemented by the image fetching handlers
    /// 
    /// Derived classes should implement FetchImage() method which is responsible for
    /// providing images from the image searching engines' APIs, which is handled by
    /// the base class Execute() method along with a simple benchmarking of the request
    /// </summary>
    public abstract class ImageFetchBase
    {
        private static readonly double _stopwatchFrequency = Stopwatch.Frequency;

        protected abstract Task<ImageFetchResult> FetchImage(ImageFetchQuery model);

        public async Task<BenchmarkResult> Execute(ImageFetchQuery model)
        {
            var stopwatch = new Stopwatch();

            // Collect the garbage before the fetching of images
            // Might slightly enhance the measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            stopwatch.Start();
            var result = await FetchImage(model);
            stopwatch.Stop();

            return new BenchmarkResult
            {
                ImageFetch = result,
                Stopwatch = stopwatch.ElapsedTicks / _stopwatchFrequency
            };
        }
    }
}