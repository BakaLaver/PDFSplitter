using Microsoft.Extensions.DependencyInjection;
using PDFSplitter.Model;
using PDFSplitter.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PDFSplitter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Настройка контейнера зависимостей
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Создание главного окна с использованием DI
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MergePDF>();
            services.AddTransient<SplitPDFFromTo>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowsModel>();
        }

    }
}
