using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using NewsletterSubscribers._Shared;
using NewsletterSubscribers._Shared.CreateNewsletterSubscriber;

namespace NewsletterSubscribers.SharedSpa;

public class NewsletterSubscriberProvider
{
    public IEnumerable<NewsletterSubscriberResponse>? NewsletterSubscribers { get; set; }

    private readonly ILogger<NewsletterSubscriberProvider> _logger;
    private readonly HttpClient _http;
    public NewsletterSubscriberProvider(ILogger<NewsletterSubscriberProvider> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
    }

    public async Task InitNewsletterSubscribers(bool? force = false)
    {
        if (NewsletterSubscribers?.Count() > 0 && force != true)
        {
            return;
        }

        NewsletterSubscribers = await GetNewsletterSubscribers();
    }

    public async Task<IEnumerable<NewsletterSubscriberResponse>?> GetNewsletterSubscribers()
    {
        try
        {
            return await _http.GetFromJsonAsync<IEnumerable<NewsletterSubscriberResponse>>(ApiRoutes.NewsletterSubscribers);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Could not get newsletter subscribers {@errorMessage}", e.Message);
            return null;
        }
    }
 
    public async Task<bool> CreateNewsletterSubscriber(CreateNewsletterSubscriberRequest request)
    {
        var response = await _http.PostAsJsonAsync(ApiRoutes.NewsletterSubscribers, request);
        return response.IsSuccessStatusCode;
    }
}