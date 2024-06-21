using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSelectLanguage : Page
    {
        string language = "Español"; // Establecer un idioma predeterminado

        public PageSelectLanguage()
        {
            InitializeComponent();
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if (app != null)
            {
                if (language == "Ingles")
                {
                    app.SetCulture("en"); // Establecer la cultura en inglés
                }
                else
                {
                    app.SetCulture(""); // Establecer la cultura predeterminada
                }
            }
        }

        private void BtnMexicoClick(object sender, RoutedEventArgs e)
        {
            language = "Español"; // Cambiar el idioma a Español al hacer clic en el botón correspondiente
        }

        private void BtnEUClick(object sender, RoutedEventArgs e)
        {
            language = "Ingles"; // Cambiar el idioma a Inglés al hacer clic en el botón correspondiente
        }
    }
}