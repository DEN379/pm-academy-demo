using RequestProcessor.App.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services
{
    internal class RequestHandler : IRequestHandler
    {
        private HttpClient client;
        
        public RequestHandler(HttpClient client)
        {
            this.client = client;
            this.client.Timeout = TimeSpan.FromSeconds(10);
        }
        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));

            using var content = new StringContent(requestOptions.Body ?? "");
            using var message = new HttpRequestMessage(MappingMethod(requestOptions.Method),
                new Uri(requestOptions.Address));
            message.Content = content;
            // content-type:
            //client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.SendAsync(message, HttpCompletionOption.ResponseContentRead);
            var asdf = Task.WhenAll(response);
            var response2 = asdf.Result;


            var rez = new Response(response2[0].IsSuccessStatusCode,
                (int)response2[0].StatusCode, response2[0].Content.ReadAsStringAsync().Result);

            return rez;
        }

        private HttpMethod MappingMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Undefined:
                    throw new ArgumentOutOfRangeException(nameof(method)+"po4emy tak");
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method));
            }
        }
    }
}
