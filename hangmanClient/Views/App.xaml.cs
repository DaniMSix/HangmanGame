using System.Globalization;
using System.Threading;
using System.Windows;

namespace Views
{
    public partial class App : Application
    {
        public App()
        {
            SetCulture("");
        }

        public void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Esto puede ser necesario si usas recursos
            Views.Properties.Resources.Culture = culture;
        }

    }
}
