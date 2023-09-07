using Mapster;
using Microsoft.EntityFrameworkCore;
using NewsletterSubscribers._Shared;
using NewsletterSubscribers.Database;

namespace NewsletterSubscribers.Features.GetNewsletterSubscribers;

public class GetNewsletterSubscribersQuery
{
    private readonly INewsletterSubscribersDbContext _dbContext;
    public GetNewsletterSubscribersQuery(INewsletterSubscribersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<NewsletterSubscriberResponse>> Handle()
    {
        return await _dbContext.NewsletterSubscribers
            .OrderBy(sub => sub.Email)
            .ProjectToType<NewsletterSubscriberResponse>()
            .ToListAsync();
    }
}