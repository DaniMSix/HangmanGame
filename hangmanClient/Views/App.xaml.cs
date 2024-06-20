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
            SetCulture(""); // Establece el idioma predeterminado aquí
        }

        public void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Asegúrate de usar el espacio de nombres correcto para los recursos
            Views.Properties.Resources.Culture = culture;

            // Recargar la ventana principal
            if (MainWindow != null)
            {
                // Recarga la ventana principal o navega a la página de inicio
                MainWindow.Content = new Login(cultureCode);
            }
        }
    }
}
