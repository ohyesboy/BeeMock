using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Plugin.Maui.Audio;

namespace BeeMock;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("fontello.ttf", "icons");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageModel>();
		builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
		var httpClient = new HttpHelper("https://zhan.blob.core.windows.net/");
       
        builder.Services.AddSingleton<HttpHelper>(httpClient);
        builder.Services.AddSingleton(AudioManager.Current);

		builder.Services.AddSingleton<ArticlePage>();
		builder.Services.AddSingleton<ArticlePageModel>();

        return builder.Build();
	}
}
