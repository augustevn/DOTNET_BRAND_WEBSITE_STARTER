using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsletterSubscribers.Features.CreateNewsletterSubscriber;
using NewsletterSubscribers.Features.GetNewsletterSubscribers;

namespace NewsletterSubscribers.Features;

public static class Extensions
{
    public static void AddNewsletterSubscribersFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<GetNewsletterSubscribersQuery>();
        
        services.AddScoped<CreateNewsletterSubscriberCommand>();
    }
}