using EFCore.Application.Services;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EfCore.UnitTests
{
    class BitCoinServiceTests
    {
        private BitCoinService _service;
        private Mock<IHttpClientFactory> mockFactory;

        public BitCoinServiceTests(BitCoinService service)
        {
            mockFactory = new Mock<IHttpClientFactory>();
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Mockk Response")
                });

            var httpClient = new HttpClient(mockMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _service = new BitCoinService(mockFactory.Object);

        }
    }
}
