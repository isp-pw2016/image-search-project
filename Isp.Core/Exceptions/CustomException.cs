using System;

namespace Isp.Core.Exceptions
{
    public class CustomException : Exception
    {
        public string Title { get; set; }

        public CustomException()
        {
        }

        public CustomException(string message, string title)
            : base(message)
        {
            Title = title;
        }

        public CustomException(string message, string title, Exception inner)
            : base(message, inner)
        {
            Title = title;
        }
    }
}