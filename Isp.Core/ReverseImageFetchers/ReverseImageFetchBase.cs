using System.IO;
using System.Threading.Tasks;
using Isp.Core.Entities;
using Isp.Core.Exceptions;

namespace Isp.Core.ReverseImageFetchers
{
    public abstract class ReverseImageFetchBase
    {
        protected abstract Task<ReverseImageFetchResult> FetchReverseImage(ReverseImageFetchQuery model, string filePath);

        public async Task<ReverseImageFetchResult> Execute(ReverseImageFetchQuery model, string uploadPath)
        {
            var filePath = Path.Combine(uploadPath, model.FileName);
            if (!File.Exists(filePath))
            {
                throw new CustomException($"Image file {model.FileName} not found on the server", "Image not found");
            }

            var result = await FetchReverseImage(model, filePath);

            return result;
        }
    }
}