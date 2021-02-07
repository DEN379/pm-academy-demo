using System;

namespace RequestProcessor.App.Models
{
    class Response : IResponse
    {
        public bool Handled { get; set; }
        public int Code { get; set; }
        public string Content { get; set; }

        public Response(bool handled, int code, string content)
        {
            Handled = handled;
            Code = code;
            Content = content;
        }
    }
}
