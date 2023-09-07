using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsletterSubscribers._Shared.CreateNewsletterSubscriber;
using NewsletterSubscribers.Database;

namespace NewsletterSubscribers.Features.CreateNewsletterSubscriber;

public class CreateNewsletterSubscriberCommand
{
    private readonly INewsletterSubscribersDbContext _dbContext;
    private readonly ILogger<CreateNewsletterSubscriberCommand> _logger;
    public CreateNewsletterSubscriberCommand(INewsletterSubscribersDbContext dbContext, ILogger<CreateNewsletterSubscriberCommand> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Handle(CreateNewsletterSubscriberRequest request)
    {
        var sanitizedEmail = request.Email.ToLower().Trim();
    
        var existingSub = await _dbContext.NewsletterSubscribers
            .FirstOrDefaultAsync(sub => sub.Email == sanitizedEmail);

        if (existingSub != null)
        {
            _logger.LogWarning("Email subscriber already exists");
            return;
        }
    
        var newSub = new NewsletterSubscriber
        {
            Email = sanitizedEmail,
            IsSubscribed = true
        };
    
        await _dbContext.NewsletterSubscribers.AddAsync(newSub);
        await _dbContext.SaveChangesAsync();
    }
}