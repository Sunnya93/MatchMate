using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using MatchMate.Page.Service;
using MatchMate.Service;

namespace MatchMate;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        builder.Services.AddMudServices();
		builder.Services.AddSingleton<MatchService>();
		builder.Services.AddScoped<HttpClientService>();
		builder.Services.AddScoped<IMessageService, MessageService>();

		return builder.Build();
	}
}
