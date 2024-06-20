using System.Globalization;
using System.Threading;
using System.Windows;
using Views.Pages;

namespace Views
{
    public partial class App : Application
    {
        public App()
        {
            // Configuración inicial de la cultura si es necesario
            SetCulture(""); // O el idioma predeterminado que desees
        }

        public void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Esto puede ser necesario si usas recursos
            Views.Properties.Resources.Culture = culture;

            // Recarga la ventana principal
            if (MainWindow != null)
            {
                MainWindow.Content = new Login(cultureCode);
            }
        }
    }
}
