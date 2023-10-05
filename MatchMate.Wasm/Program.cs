using MatchMate.Page.Service;
using MatchMate.Wasm.Service;
using MatchMate.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
builder.Services.AddSingleton<MatchService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IReadFileService, ReadFileService>();

await builder.Build().RunAsync();
