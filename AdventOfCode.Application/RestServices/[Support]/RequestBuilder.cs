using AdventOfCode.Application.Extensions;
using AdventOfCode.Application.Json;
using System.Net;
using System.Text;

namespace AdventOfCode.Application.RestServices;
public class RequestBuilder
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly Encoding _defaultEncoding = Encoding.UTF8;

    public RequestBuilder(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public HttpRequestMessage CreateUrlEncodedFormRequest(
        HttpMethod method,
        string url,
        IReadOnlyCollection<FormValue> formValues = null,
        IReadOnlyCollection<Header> customHeaders = null,
        Encoding encodingOverride = null)
    {
        HttpContent content = null;
        if (formValues != null)
        {
            var encodedPairs =
                formValues.Select(x => $"{WebUtility.UrlEncode(x.Name)}={WebUtility.UrlEncode(x.Value)}");
            var encodedData = string.Join("&", encodedPairs);
            content = new StringContent(
                encodedData,
                encodingOverride ?? _defaultEncoding,
                "application/x-www-form-urlencoded");
        }

        return CreateRequestMessage(
            method,
            url,
            customHeaders,
            content);
    }

    public HttpRequestMessage CreateMultiPartFormRequest(
        string url,
        IReadOnlyCollection<Header> customHeaders = null,
        IReadOnlyCollection<FileFormValue> fileFormValues = null
    )
    {
        var content = new MultipartFormDataContent();

        if (fileFormValues != null)
        {
            foreach (var value in fileFormValues)
            {
                content.Add(value.Content, value.FileName, value.FileName);
            }
        }

        return CreateRequestMessage(
            HttpMethod.Post,
            url,
            customHeaders,
            content);
    }

    public HttpRequestMessage CreateGetRequest(string url, IReadOnlyCollection<Header> customHeaders = null)
    {
        return CreateRequestMessage(
            HttpMethod.Get,
            url,
            customHeaders);
    }

    public HttpRequestMessage CreateDeleteRequest(string url, IReadOnlyCollection<Header> customHeaders = null)
    {
        return CreateRequestMessage(
            HttpMethod.Delete,
            url,
            customHeaders);
    }

    public HttpRequestMessage CreateDeleteJsonRequest(
        string url,
        object payload,
        IReadOnlyCollection<Header> customHeaders = null,
        bool useCamelCaseNamingStrategy = true,
        Encoding encodingOverride = null)
    {
        return CreateJsonRequest(
            HttpMethod.Delete,
            url,
            payload,
            customHeaders,
            useCamelCaseNamingStrategy,
            encodingOverride ?? _defaultEncoding);
    }

    public HttpRequestMessage CreatePostJsonRequest(
        string url,
        object payload,
        IReadOnlyCollection<Header> customHeaders = null,
        bool useCamelCaseNamingStrategy = true,
        Encoding encodingOverride = null)
    {
        return CreateJsonRequest(
            HttpMethod.Post,
            url,
            payload,
            customHeaders,
            useCamelCaseNamingStrategy,
            encodingOverride ?? _defaultEncoding);
    }

    public HttpRequestMessage CreatePutJsonRequest(
        string url,
        object payload,
        IReadOnlyCollection<Header> customHeaders = null,
        bool useCamelCaseNamingStrategy = true,
        Encoding encodingOverride = null)
    {
        return CreateJsonRequest(
            HttpMethod.Put,
            url,
            payload,
            customHeaders,
            useCamelCaseNamingStrategy,
            encodingOverride ?? _defaultEncoding);
    }

    private HttpRequestMessage CreateJsonRequest(
        HttpMethod method,
        string url,
        object payload,
        IReadOnlyCollection<Header> customHeaders,
        bool useCamelCaseNamingStrategy,
        Encoding encoding)
    {
        var json = SerializeJsonPayload(payload, useCamelCaseNamingStrategy);
        return CreateRequestMessage(
            method,
            url,
            customHeaders,
            new JsonContent(json, encoding));
    }

    protected virtual string SerializeJsonPayload(object payload, bool useCamelCaseNamingStrategy)
    {
        return _jsonSerializer.ToJsonString(payload, useCamelCaseNamingStrategy);
    }

    private HttpRequestMessage CreateRequestMessage(
        HttpMethod method,
        string url,
        IReadOnlyCollection<Header> customHeaders,
        HttpContent content = null)
    {
        var request = new HttpRequestMessage(method, url.RemoveInitialSlashIfAny());

        customHeaders.ForEach(
            header => request.Headers.Add(
                header.Name,
                header.Value));

        if (content != null)
        {
            request.Content = content;
        }

        return request;
    }
}
