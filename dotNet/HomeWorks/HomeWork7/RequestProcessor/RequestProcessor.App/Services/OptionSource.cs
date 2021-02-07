using RequestProcessor.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services
{
    class OptionSource : IOptionsSource
    {
        private string file;
        public OptionSource(string fileName)
        {
            file = fileName;
        }
        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            var options = await JsonSerializer.DeserializeAsync<IEnumerable<RequestOptions>>(fileStream);
            var result = new List<(IRequestOptions, IResponseOptions)>();

            foreach (var opt in options)
            {
                opt.Method = GetMethod(opt.RequestMethodAsString);
                result.Add((opt, opt));
            }
            return result;

        }

        private RequestMethod GetMethod(string method)
        {
            switch (method)
            {
                case "GET": return RequestMethod.Get;
                case "DELETE": return RequestMethod.Delete;
                case "PATCH": return RequestMethod.Patch;
                case "POST": return RequestMethod.Post;
                case "PUT": return RequestMethod.Put;
                default: return RequestMethod.Undefined;
            }
        }
    }
}
