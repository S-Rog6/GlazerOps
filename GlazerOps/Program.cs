using GlazerOps;
using GlazerOps.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Supabase;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Use ONE HttpClient instance for bootstrapping + DI
var bootHttp = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

// Load wwwroot/appsettings.json into Configuration
var json = await bootHttp.GetStringAsync("appsettings.json");
builder.Configuration.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for app usage
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// MudBlazor
builder.Services.AddMudServices();

builder.Services.AddScoped<LocalJobsStore>();
builder.Services.AddScoped<JobsCacheSyncService>();

// Supabase config
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:AnonKey"];

if (string.IsNullOrWhiteSpace(supabaseUrl) || string.IsNullOrWhiteSpace(supabaseKey))
    throw new InvalidOperationException("Supabase configuration missing. Check wwwroot/appsettings.json");

// Supabase client
builder.Services.AddScoped<SupabaseLocalStorageSessionHandler>();
builder.Services.AddScoped(sp =>
{
    var sessionHandler = sp.GetRequiredService<SupabaseLocalStorageSessionHandler>();
    var options = new SupabaseOptions
    {
        AutoConnectRealtime = false,
        SessionHandler = sessionHandler
    };

    return new Client(supabaseUrl, supabaseKey, options);
});

var host = builder.Build();

// Get the Supabase client from DI
var supabase = host.Services.GetRequiredService<Client>();

// IMPORTANT: initialize it
await supabase.InitializeAsync();

await host.RunAsync();