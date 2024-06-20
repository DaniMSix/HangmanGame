using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSelectLanguage : Page
    {
        System.Windows.Controls.Frame frCurrentFrame;
        System.Windows.Controls.Frame frHome;
        string language = "Ingles"; // Establecer un idioma predeterminado

        public PageSelectLanguage(System.Windows.Controls.Frame frCurrentFrame, System.Windows.Controls.Frame frHome)
        {
            InitializeComponent();
            this.frCurrentFrame = frCurrentFrame;
            this.frHome = frHome;
        }

        private void NavigateToLogin()
        {
            Login loginPage = new Login(language);
            frHome.Navigate(loginPage);
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            SetLanguage(language);
            NavigateToLogin();
            this.Visibility = Visibility.Collapsed; // Ocultar esta página después de la navegación
        }

        private void SetLanguage(string language)
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
