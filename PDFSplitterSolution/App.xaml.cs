using Microsoft.Extensions.DependencyInjection;
using PDFSplitter.BusinessLogic.BusinessModels;
using PDFSplitter.BusinessLogic.Services;
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

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Создание главного окна с использованием DI
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowsModel>();
            services.AddTransient<MergePDF>();
            services.AddTransient<SplitPDFFromTo>();
            services.AddTransient<TextCharpField>();
            services.AddTransient<PDFService>();
            services.AddTransient<PDFMergeService>();

        }

    }
}
