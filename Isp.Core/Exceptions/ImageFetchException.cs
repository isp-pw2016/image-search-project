using System;

namespace Isp.Core.Exceptions
{
    public class ImageFetchException : Exception
    {
        public string Title { get; set; }

        public ImageFetchException()
        {
        }

        public ImageFetchException(string message, string title)
            : base(message)
        {
            Title = title;
        }

        public ImageFetchException(string message, string title, Exception inner)
            : base(message, inner)
        {
            Title = title;
        }
    }
}