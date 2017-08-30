using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientTest
{
    /// <summary>
    /// Based on this code: https://stackoverflow.com/questions/22223223/how-to-pass-in-a-mocked-httpclient-in-a-net-test
    /// </summary>
    public class OffLineResponseHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> responses = new Dictionary<Uri, HttpResponseMessage>();
        public void AddResponse(Uri uri, HttpResponseMessage responseMessage)
        {
            responses.Add(uri, responseMessage);
        }
        public void AddOkResponse(Uri uri, string content)
        {
            var message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent(content);
            responses.Add(uri, message);
        }
        public void AddOkResponse(Uri uri)
        {
            this.AddOkResponse(uri, string.Empty);
        }
        public void AddServerErrorResponse(Uri uri)
        {
            var message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            responses.Add(uri, message);
        }
        public void RemoveAll()
        {
            this.responses.Clear();
        }
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (responses.ContainsKey(request.RequestUri))
            {
                return responses[request.RequestUri];
            }
            else
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request });
            }
        }
    }
}
