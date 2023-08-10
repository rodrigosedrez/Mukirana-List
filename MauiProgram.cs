using CommunityToolkit.Maui;
using LiteDB;
using Microsoft.Extensions.Logging;
using Muquirana.Services;
using Muquirana.Views;
using Plugin.MauiMTAdmob;

namespace Muquirana;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
             .UseMauiMTAdmob()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSans");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SourceCodePro-VariableFont_wght.ttf", "NovaFonte");
                fonts.AddFont("MaterialIcons-Regular.ttf", "incones");
                fonts.AddFont("Type Icons.ttf", "iconesF");
                fonts.AddFont("definitiveIcon.ttf", "defineIcon");
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "definetiveIcon");
            })
            .RegisterDatabaseAndRepositories()
            .RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LiteDatabase>(
            options =>
            {
                return new LiteDatabase($"Filename={AppSettings.DatabasePath};Connection=Shared");
            }
            );
        mauiAppBuilder.Services.AddTransient<IServiceRepository, ServiceRepository>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<Listas>();
        mauiAppBuilder.Services.AddTransient<Produtos>();
        return mauiAppBuilder;
    }
}