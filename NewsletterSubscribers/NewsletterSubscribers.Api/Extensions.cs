using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NewsletterSubscribers._Shared;
using NewsletterSubscribers._Shared.CreateNewsletterSubscriber;
using NewsletterSubscribers.Features.CreateNewsletterSubscriber;
using NewsletterSubscribers.Features.GetNewsletterSubscribers;

namespace NewsletterSubscribers.Api;

public static class Extensions
{
    public static void MapNewsletterSubscriberEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var routeGroup = routeBuilder.MapGroup(ApiRoutes.NewsletterSubscribers);

        routeGroup.MapGet("/", async (GetNewsletterSubscribersQuery query) =>
        {
            var subs = await query.Handle();
            return Results.Ok(subs);
        });
        
        routeGroup.MapPost("/", async (CreateNewsletterSubscriberRequest request, CreateNewsletterSubscriberCommand command) =>
        {
            await command.Handle(request);
            return Results.NoContent();
        });
    }
}