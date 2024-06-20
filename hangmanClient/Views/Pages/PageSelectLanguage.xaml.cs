using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Views.Pages
{
    public partial class PageSelectLanguage : Page
    {
        private string language;
        public event EventHandler MessageClosed;
        System.Windows.Controls.Frame frMessage;
        System.Windows.Controls.Frame frHome;

        public PageSelectLanguage(System.Windows.Controls.Frame frMessage, System.Windows.Controls.Frame frHome)
        {
            InitializeComponent();
            this.frMessage = frMessage;
            this.frHome = frHome;
        }

        public PageSelectLanguage()
        {
            InitializeComponent();
        }

        private void BtnAcceptClick(object sender, RoutedEventArgs e)
        {
            SetLanguage(language);
            NavigateToLogin();
        }

        private void BtnMexicoClick(object sender, RoutedEventArgs e)
        {
            language = "Español";
        }

        private void BtnEUClick(object sender, RoutedEventArgs e)
        {
            language = "Ingles";
        }

        private void SetLanguage(string language)
        {
            var app = Application.Current as App;
            if (app != null)
            {
                if (language == "Ingles")
                {
                    app.SetCulture("en");
                }
                else
                {
                    app.SetCulture("");
                }
            }
        }

        private void NavigateToLogin()
        {
            Login loginPage = new Login(language);
            NavigationService?.Navigate(loginPage);
        }
    }

}
