using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

        builder.ConfigureEssentials(essentialsBuilder =>
        {
            essentialsBuilder.UseVersionTracking(); // Enable version tracking
        });

        builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit() //used to save the pdf file in user selected location
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
