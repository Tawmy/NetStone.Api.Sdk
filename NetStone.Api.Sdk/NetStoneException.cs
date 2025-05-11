using System.Net;
using System.Net.Http.Headers;

namespace NetStone.Api.Sdk;

public class NetStoneException(
    string message,
    HttpStatusCode httpStatusCode,
    string? reasonPhrase,
    HttpResponseHeaders headers,
    HttpMethod httpMethod,
    HttpRequestMessage requestMessage,
    HttpContentHeaders? contentHeaders,
    string? content) : Exception(message)
{
    public HttpStatusCode HttpStatusCode { get; } = httpStatusCode;

    public string? ReasonPhrase { get; } = reasonPhrase;

    public HttpResponseHeaders Headers { get; } = headers;

    public HttpMethod HttpMethod { get; } = httpMethod;

    public Uri? Uri => RequestMessage.RequestUri;

    public HttpRequestMessage RequestMessage { get; } = requestMessage;

    public HttpContentHeaders? ContentHeaders { get; } = contentHeaders;

    public string? Content { get; } = content;
}