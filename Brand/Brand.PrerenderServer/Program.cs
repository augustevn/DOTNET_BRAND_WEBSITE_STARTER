using NewsletterSubscribers.Database;
using NewsletterSubscribers.Features;
using NewsletterSubscribers.SharedSpa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();

builder.Services.AddNewsletterSubscribersDbContext(builder.Configuration);
builder.Services.AddNewsletterSubscribersFeatures(builder.Configuration);
builder.Services.AddNewsletterSubscribersSharedSpa(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();