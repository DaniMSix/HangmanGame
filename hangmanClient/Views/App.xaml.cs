using System.Globalization;
using System.Threading;
using System.Windows;
using Views.Pages;
using Views.Utils;

namespace Views
{
    public partial class App : Application
    {
        public App()
        {
            var culture = new CultureInfo("");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (MainWindow != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                var mainWindowAux = MainWindow;
                MainWindow = mainWindow;
                mainWindowAux.Close();
            }
        }
    }
}
