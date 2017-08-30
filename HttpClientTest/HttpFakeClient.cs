using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientTest
{
    namespace Cellnex.Aquadocs.Mobile.Tests
    {
        /// <summary>
        /// Fake class to mock HTTP requests, it needs the Send method to be mocked in order to be used
        /// </summary>
        public class FakeHttpMessageHandler : HttpMessageHandler
        {
            public virtual HttpResponseMessage Send(HttpRequestMessage request)
            {
                throw new NotImplementedException("Mock this please");
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                return Task.FromResult(Send(request));
            }
        }
    }
}
