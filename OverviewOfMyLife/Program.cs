using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using OverviewOfMyLife;
using OverviewOfMyLife.Data;
using OverviewOfMyLife.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Bind Notion settings and add NotionService
var notionSettings = builder.Configuration.GetSection("NotionSettings").Get<NotionSettings>();
builder.Services.AddSingleton(notionSettings);

builder.Services.AddHttpClient<NotionService>(client =>
{
    client.BaseAddress = new Uri("https://api.notion.com/");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {notionSettings.ApiKey}");
    client.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
