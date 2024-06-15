using Microsoft.Extensions.Logging;

namespace TastyTales
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<Data.IRepository, Data.DatabaseRepository>();
            builder.Services.AddSingleton<Services.IDataServices, Services.DataServices>();
            builder.Services.AddTransient<ViewModels.ISearchRecipeViewModel, ViewModels.SearchRecipeViewModel>();
            builder.Services.AddTransient<ViewModels.IMainPage, ViewModels.MainPage>();
            builder.Services.AddTransient<ViewModels.ICategoryPageViewModel, ViewModels.CategoryPage>();
            builder.Services.AddTransient<Views.SearchPage>();
            builder.Services.AddTransient<Views.HomePage>();
            builder.Services.AddTransient<Views.CategoriesPage>();
            return builder.Build();
        }
    }
}
